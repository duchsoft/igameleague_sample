using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ScoreboardSortType
{
    CreatedDate,
    ClosingDate,
    JoiningPoint,
    Sponsored
}
;

public enum SortOrder
{
    ASC,
    DESC
}
;

public class LeaderboardsScript : MonoBehaviour
{
    ScoreboardSortType SortType;
    SortOrder Order;

    public int ButtonFontSize = 20;
    public int VisibleItemsCountInOnePage = 5;
    public int PerTileHeight = 130;
    int m_childCount = 0;
    int m_panelHeight;

    const int ItemPerPages = 5;
    int m_PageNo = 1;

    public bool NeedScrolling = false;

    public GameObject ListerPanel;
    public GameObject ListerPanel_Fav;
    public GameObject ListerPanel_Share;
    public GameObject SortBar;
    public GameObject Scrollbar;
    public Button ForwardButton;
    public Button BackwardButton;
    public Text TitleText;
    public InputField tournamentID;
    public Text Message;
    public Toggle DateToggle;
    public Toggle JoiningPointToggle;
    public Toggle SponsoredToggle;
    private bool ToggleIsOn;

    void Start()
    {
        m_PageNo = 1;

        SortType = (ScoreboardSortType)PlayerPrefs.GetInt("SortType", 1);
        Order = (SortOrder)PlayerPrefs.GetInt("SortOrder", 1);


        if (Order == SortOrder.ASC)
        {
            ToggleIsOn = true;
        }
        else
        {
            ToggleIsOn = false;
        }

        if (SortType == ScoreboardSortType.ClosingDate)
        {
            DoDateToggle(ToggleIsOn);
        }
        if (SortType == ScoreboardSortType.JoiningPoint)
        {
            DoJoiningPointToggle(ToggleIsOn);
        }
        if (SortType == ScoreboardSortType.Sponsored)
        {
            DoSponsoredToggle(ToggleIsOn);
        }

        Refresh();
    }

    void CanInteract(bool val)
    {
        if (val)
        {
            DateToggle.interactable = true;
            JoiningPointToggle.interactable = true;
            SponsoredToggle.interactable = true;
        }
        else
        {
            DateToggle.interactable = false;
            JoiningPointToggle.interactable = false;
            SponsoredToggle.interactable = false;
        }
    }

    private void Refresh()
    {
        for (int i = 0, childCount = ListerPanel.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(ListerPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0, childCount = ListerPanel_Fav.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(ListerPanel_Fav.transform.GetChild(i).gameObject);
        }

        for (int i = 0, childCount = ListerPanel_Share.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(ListerPanel_Share.transform.GetChild(i).gameObject);
        }


        ForwardButton.interactable = false;
        BackwardButton.interactable = false;
        UIStateManager.Manager.SetLoading(true);
        CanInteract(false);

        StartCoroutine(
            UIStateManager.GameAPI.GetAllLeaderboardsForPlayerToJoin(
            userName: UIStateManager.Manager.PlayerId,
            gameId: UIStateManager.Manager.GameID,
            sortBy: (int)SortType,
            sortOrder: (int)Order,
            inviteOnly: false,
            startIndex: ((ItemPerPages * m_PageNo) - ItemPerPages) + 1,
            endIndex: (ItemPerPages * m_PageNo) + 1,
            onGetLeaderboardDetail: OnGetLeaderboardsDetails));
    }

    void OnGetLeaderboardsDetails(string errorString, LeaderboardDetail[] result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            int index = 0;

            foreach (LeaderboardDetail item in result)
            {
                if (index < ItemPerPages)
                {
                    AddLeaderboardEntry(item);
                }
                index++;
            }

            if (m_PageNo > 1)
            {
                BackwardButton.interactable = true;
            }

            if (result.Length == ItemPerPages + 1)
            {
                ForwardButton.interactable = true;
            }
        }
        else
        {
            m_PageNo = 1;
        }
        UIStateManager.Manager.SetLoading(false);
        CanInteract(true);
        PlayerPrefs.Save();
        if (NeedScrolling)
        {
            StartCoroutine(RefreshPanelSize());
        }
    }

    private void AddLeaderboardEntry(LeaderboardDetail item)
    {
        GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemTemplate_LeaderboardTiles);
        GameObject obj_2 = GameObject.Instantiate(UIStateManager.Manager.FavouritesToggleTemplate);
        GameObject obj_3 = GameObject.Instantiate(UIStateManager.Manager.ListItemSelectionTemplate);
        obj.transform.SetParent(ListerPanel.transform, false);
        obj_2.transform.SetParent(ListerPanel_Fav.transform, false);
        obj.name = item.Id;

        obj_3.transform.SetParent(ListerPanel_Share.transform, false);

        Button objButton = obj.GetComponent<Button>();
        obj.GetComponentInChildren<Text>().fontSize = ButtonFontSize;

        UIStateManager.Manager.SetLeaderboardName(item, objButton);
        Toggle ToggleFavourite = obj_2.GetComponent<Toggle>();
        ToggleFavourite.isOn = false;

        Button shareButton = obj_3.GetComponent<Button>();
        shareButton.GetComponent<Image>().sprite = UIStateManager.Manager.ShareTexture;

        LeaderboardStorage[] storage = UIStateManager.Manager.SettingsSystem.GetFavs(UIStateManager.Manager.PlayerId);

        foreach (LeaderboardStorage itemFav in storage)
        {
            if (itemFav.Leaderboard.Id == item.Id)
            {
                ToggleFavourite.isOn = true;
                break;
            }
            else
            {
                ToggleFavourite.isOn = false;
            }
        }

        AddEventToButton(item, objButton, shareButton);
        AddEventToToggle(item, ToggleFavourite);
    }

    private void AddEventToButton(LeaderboardDetail item, Button objButton, Button shareButton)
    {
        objButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            UIStateManager.Manager.SwapToKeyValidationPage(null, null, item, KeyValiationStatesBack.LeaderBoards);
        });

        shareButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            Debug.Log("Implement your share mechanism here");
        });
    }

    private void AddEventToToggle(LeaderboardDetail item, Toggle favToggle)
    {
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
    }

    IEnumerator RefreshPanelSize()
    {
        yield return null;
        m_childCount = ListerPanel.transform.childCount;

        if (m_childCount > VisibleItemsCountInOnePage)
        {
            Scrollbar.SetActive(true);
        }
        Debug.Log(m_childCount);
        m_panelHeight = m_childCount * PerTileHeight;
        ListerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Fav.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
        ListerPanel_Share.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_panelHeight);
    }

    public void SelectDateToggle()
    {
        DoDateToggle(DateToggle.isOn);

        SortType = ScoreboardSortType.ClosingDate;
        PlayerPrefs.SetInt("SortType", (int)ScoreboardSortType.ClosingDate);
        Refresh();
    }

    void DoDateToggle(bool val)
    {

        DateToggle.transform.FindChild("Background").gameObject.SetActive(true);
        JoiningPointToggle.transform.FindChild("Background").gameObject.SetActive(false);
        SponsoredToggle.transform.FindChild("Background").gameObject.SetActive(false);

        if (val)
        {
            Order = SortOrder.ASC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.ASC);
        }
        else
        {
            Order = SortOrder.DESC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.DESC);
        }
        PlayerPrefs.Save();
    }

    public void SelectJoiningPointToggle()
    {
        DoJoiningPointToggle(JoiningPointToggle.isOn);
        SortType = ScoreboardSortType.JoiningPoint;
        PlayerPrefs.SetInt("SortType", (int)ScoreboardSortType.JoiningPoint);
        Refresh();
    }

    void DoJoiningPointToggle(bool val)
    {
        JoiningPointToggle.transform.FindChild("Background").gameObject.SetActive(true);
        DateToggle.transform.FindChild("Background").gameObject.SetActive(false);
        SponsoredToggle.transform.FindChild("Background").gameObject.SetActive(false);

        if (val)
        {
            Order = SortOrder.ASC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.ASC);
        }
        else
        {
            Order = SortOrder.DESC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.DESC);
        }
        PlayerPrefs.Save();
    }

    public void SelectSponsoredToggle()
    {
        DoSponsoredToggle(SponsoredToggle.isOn);
        SortType = ScoreboardSortType.Sponsored;
        PlayerPrefs.SetInt("SortType", (int)ScoreboardSortType.Sponsored);
        Refresh();
    }

    void DoSponsoredToggle(bool val)
    {
        SponsoredToggle.transform.FindChild("Background").gameObject.SetActive(true);
        DateToggle.transform.FindChild("Background").gameObject.SetActive(false);
        JoiningPointToggle.transform.FindChild("Background").gameObject.SetActive(false);

        if (val)
        {
            Order = SortOrder.ASC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.ASC);
        }
        else
        {
            Order = SortOrder.DESC;
            PlayerPrefs.SetInt("SortOrder", (int)SortOrder.DESC);
        }
        PlayerPrefs.Save();
    }

    public void SelectJoinTournament()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        string tId = tournamentID.text.Trim();
        if (!string.IsNullOrEmpty(tId))
        {
            UIStateManager.Manager.SetLoading(true);
            StartCoroutine(UIStateManager.GameAPI.GetLeaderboardsByTagFromGame(
                userName: UIStateManager.Manager.PlayerId,
                gameId: UIStateManager.Manager.GameID,
                tag: tId,
                activeOnly: true,
                startIndex: 1,
                endIndex: 100,
                onGetLeaderboardDetail: OnGetJoinLeaderboardsDetail));
        }
    }

    void OnGetJoinLeaderboardsDetail(string errorString, LeaderboardDetail[] result, params object[] userParam)
    {
        if (result != null && result.Length > 0 && result[0].Active)
        {
            StartCoroutine(UIStateManager.GameAPI.GetGameLevelDetails(result[0].ParentLevelId, OnGetGameLevelDetail, result));
        }
        else
        {
            Message.text = "Please enter a valid tournament ID";
        }
        UIStateManager.Manager.SetLoading(false);
    }

    void OnGetGameLevelDetail(string errorString, GameLevelDetail result, params object[] userParam)
    {
        if (result != null && result.Active)
        {
            LeaderboardDetail leaderboardDetail = userParam[0] as LeaderboardDetail;

            UIStateManager.Manager.SwapToKeyValidationPage(null, null, leaderboardDetail, KeyValiationStatesBack.UserTournament);
        }
    }

    public void SelectForward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        m_PageNo++;
        Refresh();
    }

    public void SelectBackward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        if (m_PageNo > 1)
        {
            m_PageNo--;
        }
        Refresh();
    }

    public void SelectBack()
    {
        UIStateManager.Manager.SetLoading(false);
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToTournamentPage();
        PlayerPrefs.Save();
    }
}
