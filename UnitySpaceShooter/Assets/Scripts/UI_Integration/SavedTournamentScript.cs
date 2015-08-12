using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SavedTournamentScript : MonoBehaviour
{
	public GameObject ListerPanel_Leaderboards;
    public GameObject ListerPanel_Fav;
    public GameObject ListerPanel_Share;
	public GameObject Scrollbar;
    public int ButtonFontSize = 18;
    public int VisibleItemsCountInOnePage = 4;
    public int PerButtonHeight = 130;
    public GameObject Message;

	int m_childCount = 0;
	int m_panelHeight = 290;
    

    public static GameLevelDetail GrandParentLevel;
    public static GameLevelDetail Level;

    //public Button OtherButton;
    void Start()
    {
        Scrollbar.SetActive(false);
        if (!String.IsNullOrEmpty(UIStateManager.Manager.PlayerId))
        {
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(UIStateManager.Manager.PlayerId, OnAuthentication));
        }
        UIStateManager.Manager.OnConnectionChange += Manager_OnConnectionChange;
    }

	void OnAuthentication (string errorString, bool result, params object[] userParams)
	{
		if (string.IsNullOrEmpty (errorString)) {
			if (UIStateManager.Manager.SettingsSystem.GetLeaderboardCache (UIStateManager.Manager.PlayerId).Length == 0) {
                Message.SetActive(true);
			} else {
				PopulateLeaderboards ();
			}
		} else {
			PopulateLeaderboards ();
		}
	}

    void OnDestroy()
    {
        UIStateManager.Manager.OnConnectionChange -= Manager_OnConnectionChange;
    }

    void Manager_OnConnectionChange(bool online)
    {
        //OtherButton.interactable = online;
    }

    void PopulateLeaderboards ()
	{
        Message.SetActive(false);
		LeaderboardStorage[] storage = UIStateManager.Manager.SettingsSystem.GetLeaderboardCache (UIStateManager.Manager.PlayerId);
		foreach (LeaderboardStorage item in storage) {
			GameObject obj = GameObject.Instantiate (UIStateManager.Manager.ListItemTemplate_LeaderboardTiles);
            GameObject obj_2 = GameObject.Instantiate(UIStateManager.Manager.FavouritesToggleTemplate);
            GameObject obj_3 = GameObject.Instantiate(UIStateManager.Manager.ListItemSelectionTemplate);
            obj.GetComponentInChildren<Text>().fontSize = ButtonFontSize;
            obj.transform.SetParent (ListerPanel_Leaderboards.transform, false);
            obj_2.transform.SetParent(ListerPanel_Fav.transform, false);
			obj.name = item.Leaderboard.Name;
            obj_3.transform.SetParent(ListerPanel_Share.transform, false);

            Button shareButton = obj_3.GetComponent<Button>();
            shareButton.GetComponent<Image>().sprite = UIStateManager.Manager.ShareTexture;
        
			Button objButton = obj.GetComponent<Button> ();
			UIStateManager.Manager.SetLeaderboardName (item.Leaderboard, objButton);

            Toggle ToggleFavourite = obj_2.GetComponent<Toggle>();
            ToggleFavourite.isOn = false;

            LeaderboardStorage[] storageFavs = UIStateManager.Manager.SettingsSystem.GetFavs(UIStateManager.Manager.PlayerId);
            
            foreach (LeaderboardStorage itemFav in storageFavs)
            {
                if (itemFav.Leaderboard.Id == item.Leaderboard.Id)
                {
                    ToggleFavourite.isOn = true;
                    break;
                }
                else
                {
                    ToggleFavourite.isOn = false;
                }
            }

            AddEventToButton(item.Leaderboard, objButton, ToggleFavourite, shareButton);
		}

        StartCoroutine(RefreshPanelSize());
	}

    IEnumerator RefreshPanelSize()
    {
        yield return null;
        m_childCount = ListerPanel_Leaderboards.transform.childCount;

        if (m_childCount > VisibleItemsCountInOnePage)
        {
            Scrollbar.SetActive(true);
        }
        m_panelHeight = m_childCount * PerButtonHeight;
        ListerPanel_Leaderboards.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Fav.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Share.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
    }

    private void AddEventToButton(LeaderboardDetail item, Button objButton, Toggle favToggle, Button shareButton)
    {
        objButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            UIStateManager.Manager.SwapToKeyValidationPage(null, null, item, KeyValiationStatesBack.MyTournaments);
        });

        favToggle.onValueChanged.AddListener((isOn) =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            if (isOn)
            {
                UIStateManager.Manager.SettingsSystem.AddToLeaderboardCache(UIStateManager.Manager.PlayerId, item, "", true);
            }
            else
            {
                UIStateManager.Manager.SettingsSystem.RemoveFromFav(UIStateManager.Manager.PlayerId, item.Id);
            }
        });

        shareButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            Debug.Log("Implement your share mechanism here");
        });
    }

	public void SelectOther ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToChallengesPage ();
	}

    public void SelectFavorites()
    {
        UIStateManager.Manager.SwapToFavouritesPage();
    }


	public void SelectBack ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToTournamentPage ();
	}
}