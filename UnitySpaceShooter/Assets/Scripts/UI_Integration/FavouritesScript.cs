using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FavouritesScript : MonoBehaviour
{
    public GameObject ListerPanel_Leaderboards;
    public GameObject ListerPanel_Remove;
    public GameObject ListerPanel_Share;
    public GameObject Scrollbar;
    public GameObject Message;
    public int ButtonFontSize = 18;
	public int VisibleItemsCountInOnePage=5;
    int m_childCount = 0;
    int m_panelHeight;
	public int PerButtonHeight = 130;

    void Start()
    {
        Scrollbar.SetActive(false);
        PopulateLeaderboards();
    }

    void AddLeaderboardEntry(LeaderboardDetail leaderboardDetail)
    {
        GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemTemplate_LeaderboardTiles);
        GameObject obj_2 = GameObject.Instantiate(UIStateManager.Manager.ListItemSelectionTemplate);
        GameObject obj_3 = GameObject.Instantiate(UIStateManager.Manager.ListItemSelectionTemplate);
        obj.transform.SetParent(ListerPanel_Leaderboards.transform, false);
        obj.GetComponentInChildren<Text>().fontSize = ButtonFontSize;
        obj_2.transform.SetParent(ListerPanel_Remove.transform, false);
        obj_3.transform.SetParent(ListerPanel_Share.transform, false);
        obj.name = leaderboardDetail.Id;

        Button objButton = obj.GetComponent<Button>();
        UIStateManager.Manager.SetLeaderboardName(leaderboardDetail, objButton);

        Button favButton = obj_2.GetComponent<Button>();
        Image favSprite = favButton.GetComponentInChildren<Image>();
        favSprite.sprite = UIStateManager.Manager.RemoveTexture;
        Button shareButton = obj_3.GetComponent<Button>();
        shareButton.GetComponent<Image>().sprite = UIStateManager.Manager.ShareTexture;
        AddEventToButton(leaderboardDetail, objButton, favButton, shareButton);
    }

    private void AddEventToButton(LeaderboardDetail item, Button objButton, Button favButton, Button shareButton)
    {
        objButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            UIStateManager.Manager.SwapToKeyValidationPage(null, null, item, KeyValiationStatesBack.FromFav);
        });

        favButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            UIStateManager.Manager.SettingsSystem.RemoveFromFav(UIStateManager.Manager.PlayerId, item.Id);
            GameObject.DestroyImmediate(objButton.gameObject);
            GameObject.DestroyImmediate(favButton.gameObject);
            GameObject.DestroyImmediate(shareButton.gameObject);

            StartCoroutine(RefreshPanelSize());

            if (ListerPanel_Leaderboards.transform.childCount <= 0)
            {
                Message.SetActive(true);
            }
        });

        shareButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            Debug.Log("Implement your share mechanism here");
        });

    }

    void PopulateLeaderboards()
    {

        LeaderboardStorage[] storage = UIStateManager.Manager.SettingsSystem.GetFavs(UIStateManager.Manager.PlayerId);
        
        Message.SetActive(true);
        foreach (LeaderboardStorage item in storage)
        {
            Message.SetActive(false);
            AddLeaderboardEntry(item.Leaderboard);
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
        else
        {
            Scrollbar.SetActive(false);
        }
        m_panelHeight = m_childCount * PerButtonHeight;
        ListerPanel_Leaderboards.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Remove.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Share.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToSavedTournamentsPage();
    }
}