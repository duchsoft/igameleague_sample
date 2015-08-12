using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

public class LeaderboardListScript : MonoBehaviour
{
    public static LeaderboardDetail LeaderboardDetail;
    public GameObject LeaderBoardNameLister;
    public GameObject LeaderBoardScoreLister;
    public GameObject CurrentScoreObject;
    public GameObject WinningPlayersObject;
    public RawImage BannerArea;
    public Button ForwardButton;
    public Button BackwardButton;
    public Button PlayButton;
    public Toggle LeaderboardToggle;

    public Image ShareButtonIcon;
    public Sprite MailIcon;
    public Sprite ShareIcon;

    public static double Score = 0;

    public static bool GotErrorDuringScoreUpdate = false;

    public Text CurrentScoreText;
    public Text BestScoreText;
    public Text RankText;
    public Text TournamentText;
    public Text TournamentValidity;
    public Text WinningPlayers;
    public Text RewardPoints;

    public Text TitleName;
    public Text LeftPanelName;
    public Text ToggleButtonName;

    public Image ToggleConnectionMode;
    public Sprite OnlineModeSprite;
    public Sprite OfflineModeSprite;
    public static string PlayerBestScore;

    const int ItemPerPages = 5;
    int m_PageNo = 1;
    string ToggledOnTitleScoreboard = "MY SCOREBOARD";
    string ToggledOnLeftPanel = "Time";
    string ToggledOffTitleLeaderBoard = "LEADERBOARD";
    string ToggledOffLeftPanel = "User Names";
    string ToggledOnButtonName = "My Scoreboard";
    string ToggledOffButtonName = "Leaderboard";

    const string NotAvailable = "Not Available";
    bool m_Online = true;
    bool drawAd = false;
    Rect InterstitialRect;
    Rect AdCloseButtonRect;
    Rect BackgroundTintRect;
    public Texture2D AdCloseButtonTexture;
    public Texture2D BackgroundTint;
    static Texture2D InterstitialTexture = null;
    static Texture2D BannerTexture = null;
    static string InterstitialClickURL = string.Empty;
    static string BannerClickURL = string.Empty;
    public GUIStyle style;
    GraphicRaycaster graphicRaycaster;
    public float timer;

    // Use this for initialization
    void Start()
    {
        timer = 5;
        graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        //InterstitialRect = new Rect((Screen.width - InterstitialPortraitSize.x) / 2, (Screen.height - InterstitialPortraitSize.y) / 2, InterstitialPortraitSize.x, InterstitialPortraitSize.y);
        InterstitialRect = new Rect(0, 0, Screen.width, Screen.height);
        float CloseButtonWidth = Screen.width * 0.1f;
        AdCloseButtonRect = new Rect(Screen.width - CloseButtonWidth, 0, CloseButtonWidth, CloseButtonWidth);
        BackgroundTintRect = new Rect(-2, -2, Screen.width + 4, Screen.height + 4);
        m_PageNo = 1;
        BestScoreText.text = NotAvailable;
        RankText.text = NotAvailable;

        m_Online = !GotErrorDuringScoreUpdate;
        StartCoroutine(
            UIStateManager.GameAPI.GetPlayerRankDetail(
               UIStateManager.Manager.PlayerId,
               LeaderboardDetail.Id,
               OnGetRankDetails));

        if (GotErrorDuringScoreUpdate)
        {
            DoLeaderBoardToggle(false);
            LeaderboardToggle.interactable = false;
        }
        else
        {
            Refresh(true);
        }

        if (!LeaderboardDetail.Active)
        {
            PlayButton.interactable = false;
        }

        CurrentScoreObject.SetActive(true);
        WinningPlayersObject.SetActive(true);
        TournamentText.text = LeaderboardListScript.LeaderboardDetail.Name;
        TournamentValidity.text = UIStateManager.Manager.GetLeaderboardValidity(LeaderboardDetail, LeaderboardDetail.LifeTime / 60).ToString() +
            (LeaderboardDetail.Active ? "" : " Expired");

        UIStateManager.Manager.OnConnectionChange += Manager_OnConnectionChange;

        if (UIStateManager.Manager.ShowScore)
        {
            CurrentScoreObject.SetActive(true);
        }
        else
        {
            CurrentScoreObject.SetActive(false);
        }

        if (LeaderboardDetail.IsSponsored)
        {
            LoadInterstitial();
            drawAd = true;
        }

#if UNITY_ANDROID || UNITY_IPHONE
        ShareButtonIcon.sprite = ShareIcon;
#else 
		ShareButtonIcon.sprite = MailIcon;
#endif

    }

    void Manager_OnConnectionChange(bool online)
    {
        if (m_Online != online)
        {
            if (online)
            {
                GotErrorDuringScoreUpdate = false;
                LeaderboardToggle.interactable = true;
                ToggleConnectionMode.sprite = OnlineModeSprite;
                StartCoroutine(
                UIStateManager.GameAPI.GetPlayerRankDetail(
                   UIStateManager.Manager.PlayerId,
                   LeaderboardDetail.Id,
                   OnGetRankDetails));
                if (UIStateManager.Manager.ShowScore)
                {
                    CurrentScoreObject.SetActive(true);
                }
                else
                {
                    CurrentScoreObject.SetActive(false);
                }
                WinningPlayersObject.SetActive(true);

            }
            else
            {
                GotErrorDuringScoreUpdate = true;

                LeaderboardToggle.isOn = false;
                LeaderboardToggle.interactable = false;
                ToggleConnectionMode.sprite = OfflineModeSprite;
                CurrentScoreObject.SetActive(false);
                WinningPlayersObject.SetActive(false);
                RankText.text = NotAvailable;
            }
        }

        m_Online = online;
    }

    private void Refresh(bool leaderBaordOn)
    {
        for (int i = 0, childCount = LeaderBoardNameLister.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(LeaderBoardNameLister.transform.GetChild(i).gameObject);
        }

        for (int i = 0, childCount = LeaderBoardScoreLister.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(LeaderBoardScoreLister.transform.GetChild(i).gameObject);
        }

        ForwardButton.interactable = false;
        BackwardButton.interactable = false;
        CurrentScoreText.text = Score.ToString();

        LeaderboardListScript.SetBanner(BannerArea, LeaderboardDetail);

        UIStateManager.Manager.SettingsSystem.AddToLeaderboardCache(
            UIStateManager.Manager.PlayerId,
            UIStateManager.Manager.CurrentLeaderboardDetail,
            UIStateManager.Manager.CurrentActivationKey,
            false);

        LeaderboardStorage storage = UIStateManager.Manager.SettingsSystem.GetLeaderboardCache(UIStateManager.Manager.PlayerId, LeaderboardListScript.LeaderboardDetail.Id);
        if (storage != null)
        {
            LeaderboardListScript.LeaderboardDetail = storage.Leaderboard;
            WinningPlayers.text = "Top " + LeaderboardDetail.TopWinnersCount.ToString();
            RewardPoints.text = LeaderboardDetail.TotalRewardPoints.ToString() + "+";
        }

        if (leaderBaordOn)
        {
            UIStateManager.Manager.SetLoading(true);
            StartCoroutine
            (
                UIStateManager.GameAPI.GetLeaderboardCurrentStatus
                (
                    LeaderboardDetail.Id,
                    ((ItemPerPages * m_PageNo) - ItemPerPages) + 1,
                    (ItemPerPages * m_PageNo) + 1,
                    OnGetLeaderboardStatus,
                    LeaderboardDetail)
            );
        }
        else
        {
            StartCoroutine(PropagateScoreBoard());
        }
    }

    IEnumerator PropagateScoreBoard()
    {
        List<ScoreHistoryItem> history = UIStateManager.Manager.GetUserScoreHistory(LeaderboardDetail);
        history.Reverse();


        int start = ((ItemPerPages * m_PageNo) - ItemPerPages);
        int end = (ItemPerPages * m_PageNo);

        int length = history.Count;
        if (length > 0)
        {
            for (int index = start; index < end; index++)
            {
                if (index < length)
                {
                    AddItem(LeaderBoardNameLister, history[index].TimeStamp);
                    AddItem(LeaderBoardScoreLister, history[index].Score.ToString());

                    yield return history[index];
                }
            }
            if (m_PageNo > 1)
            {
                BackwardButton.interactable = true;
            }

            if (end < length)
            {
                ForwardButton.interactable = true;
            }

        }
        else
        {
            m_PageNo = 1;
        }
    }

    //public static void SetBanner(RawImage banner)
    //{
    //    if (UIStateManager.Manager.CurrentGamePlayMode)
    //    {
    //        UIStateManager.Manager.StartCoroutine
    //        (
    //            UIStateManager.GameAPI.GetBannerDetails
    //            (
    //                UIStateManager.Manager.PlayerId,
    //                UIStateManager.Manager.CurrentLeaderboardDetail.Id,
    //                LeaderboardListScript.OnGetBannerDetails,
    //                banner
    //            )
    //        );
    //    }
    //}

    public static void SetBanner(RawImage banner, LeaderboardDetail detail)
    {
        if (UIStateManager.Manager.CurrentGamePlayMode)
        {
            if (UIStateManager.Manager.CurrentLeaderboardDetail.IsSponsored)
            {
                string BannerFilePath;
                string BannerUrlSaveName;
                BannerUrlSaveName = detail.Id + "_Banner";
                BannerFilePath = Path.Combine(Application.persistentDataPath, detail.Id + "_Banner.png");
                byte[] dataToLoad;
                if (File.Exists(BannerFilePath))
                {
                    dataToLoad = File.ReadAllBytes(BannerFilePath);
                    BannerTexture = new Texture2D(320, 50);
                    BannerTexture.LoadImage(dataToLoad);
                    banner.texture = BannerTexture;
                }

                if (PlayerPrefs.HasKey(BannerUrlSaveName))
                {
                    BannerClickURL = PlayerPrefs.GetString(BannerUrlSaveName);
                }

                if (banner.gameObject != null && BannerTexture != null)
                {
                    banner.gameObject.SetActive(true);
                }
                else
                {
                    banner.gameObject.SetActive(false);
                }

                Button button = banner.gameObject.GetComponent<Button>();
                button.onClick.RemoveAllListeners();

                if (!string.IsNullOrEmpty(BannerClickURL))
                {
                    button.onClick.AddListener(() =>
                    {
                        Application.OpenURL(BannerClickURL);
                    });
                }
            }
        }
    }

    public static void LoadInterstitial()
    {
        string InterstitialFilePath;
        string InterstitialUrlSaveName = LeaderboardDetail.Id + "_Interstitial";

        byte[] dataToLoad;
        InterstitialFilePath = Path.Combine(Application.persistentDataPath, LeaderboardDetail.Id + "_Interstitial.png");

        if (File.Exists(InterstitialFilePath))
        {
            dataToLoad = File.ReadAllBytes(InterstitialFilePath);
            if (UIStateManager.Manager.GameOrientation == GameOrientation.Portrait)
            {
                InterstitialTexture = new Texture2D(640, 960);
            }
            else
            {
                InterstitialTexture = new Texture2D(960, 640);
            }
            InterstitialTexture.LoadImage(dataToLoad);
        }

        if (PlayerPrefs.HasKey(InterstitialUrlSaveName))
        {
            InterstitialClickURL = PlayerPrefs.GetString(InterstitialUrlSaveName);
        }
    }

    void OnGUI()
    {
        if (InterstitialTexture != null && drawAd)
        {
            graphicRaycaster.enabled = false;

            if (GUI.Button(AdCloseButtonRect, AdCloseButtonTexture))
            {
                DisableAd();
            }

            GUI.DrawTexture(BackgroundTintRect, BackgroundTint);

            GUI.DrawTexture(InterstitialRect, InterstitialTexture, ScaleMode.ScaleToFit, true);
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "", style))
            {
                if (!string.IsNullOrEmpty(InterstitialClickURL))
                {
                    Application.OpenURL(InterstitialClickURL);
                }
            }
            GUI.DrawTexture(AdCloseButtonRect, AdCloseButtonTexture);

            Invoke("DisableAd", 5);
        }
    }

    void DisableAd()
    {
        graphicRaycaster.enabled = true;
        drawAd = false;
    }

    static void OnGetBannerDetails(string errorString, BannerDetail result, params object[] userParam)
    {
        RawImage bannerArea = userParam[0] as RawImage;
        if (bannerArea.gameObject != null)
        {
            bannerArea.gameObject.SetActive(false);
        }

        UIStateManager.Manager.SettingsSystem.AddToLeaderboardCache(
            UIStateManager.Manager.PlayerId,
            UIStateManager.Manager.CurrentLeaderboardDetail,
            UIStateManager.Manager.CurrentActivationKey,
            false);

        if (string.IsNullOrEmpty(errorString))
        {
            if (result != null)
            {
                if (bannerArea.gameObject != null)
                    bannerArea.gameObject.SetActive(true);

                UIStateManager.Manager.StartCoroutine(UIStateManager.Manager.SetBannerImage(result.BannerUrl, bannerArea));

                Button button = bannerArea.gameObject.GetComponent<Button>();
                button.onClick.RemoveAllListeners();

                if (!string.IsNullOrEmpty(result.BannerClickUrl))
                {
                    button.interactable = true;
                    button.onClick.AddListener(() =>
                    {
                        Application.OpenURL(result.BannerClickUrl);
                    });
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
    }

    void OnGetRankDetails(string errorString, RankDetail result, params object[] userParam)
    {
        RankText.text = "Not Available";
        if (string.IsNullOrEmpty(errorString))
        {
            BestScoreText.text = "0";
            RankText.text = "0";

            if (result != null)
            {
                ToggleConnectionMode.sprite = OnlineModeSprite;
                BestScoreText.text = result.Score.ToString();
                PlayerBestScore = result.Score.ToString();
                RankText.text = result.Rank.ToString();
            }
        }
        else
        {
            SetHighScoreOffline(LeaderboardDetail);
        }
    }

    void OnGetLeaderboardStatus(string errorString, LeaderboardStatus result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            string[] playerDetails = result.PlayerUserNames;
            double[] scoreDetails = result.Scores;
            WinningPlayers.text = "Top " + result.TopWinnersCount.ToString();
            if (playerDetails != null && scoreDetails != null)
            {
                long length = playerDetails.Length;
                if (length > 0 && scoreDetails.Length == length)
                {
                    for (int index = 0; index < length; index++)
                    {
                        if (index < ItemPerPages)
                        {
                            AddItem(LeaderBoardNameLister, playerDetails[index]);
                            AddItem(LeaderBoardScoreLister, scoreDetails[index].ToString());
                        }
                    }
                    if (m_PageNo > 1)
                    {
                        BackwardButton.interactable = true;
                    }

                    if (length == ItemPerPages + 1)
                    {
                        ForwardButton.interactable = true;
                    }

                }
                else
                {
                    m_PageNo = 1;
                }
            }
            else
            {
                m_PageNo = 1;
            }
        }
        else
        {
            m_PageNo = 1;
        }
        UIStateManager.Manager.SetLoading(false);
    }

    void SetHighScoreOffline(LeaderboardDetail detail)
    {
        ToggleConnectionMode.sprite = OfflineModeSprite;
        BestScoreText.text = UIStateManager.Manager.ScoreHistorySystem.GetHighScore(UIStateManager.Manager.PlayerId, detail).ToString();
    }

    private void AddItem(GameObject lister, string name)
    {
        GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemsTemplate_LeaderboardScores);
        obj.transform.SetParent(lister.transform, false);
        obj.GetComponent<Button>().interactable = false;

        Button objButton = obj.GetComponent<Button>();
        Text objText = objButton.GetComponentInChildren<Text>();

        obj.name = objText.text = name;
    }

    public void SelectForward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        m_PageNo++;

        if (GotErrorDuringScoreUpdate)
        {
            Refresh(false);
        }
        else
        {
            Refresh(LeaderboardToggle.isOn);
        }

    }

    public void SelectBackward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        if (m_PageNo > 1)
        {
            m_PageNo--;
        }
        if (GotErrorDuringScoreUpdate)
        {
            Refresh(false);
        }
        else
        {
            Refresh(LeaderboardToggle.isOn);
        }
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToTournamentPage();
    }

    public void DoLeaderBoardToggle(bool val)
    {
        m_PageNo = 1;
        if (val)
        {
            TitleName.text = ToggledOffTitleLeaderBoard;
            LeftPanelName.text = ToggledOffLeftPanel;
            ToggleButtonName.text = ToggledOnButtonName;
        }
        else
        {
            TitleName.text = ToggledOnTitleScoreboard;
            LeftPanelName.text = ToggledOnLeftPanel;
            ToggleButtonName.text = ToggledOffButtonName;
        }
        Refresh(val);
    }

    public void ToggleLeaderboardState()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        DoLeaderBoardToggle(LeaderboardToggle.isOn);
    }

    public void SelectRetry()
    {
        Time.timeScale = 1.0f;
        UIStateManager.Manager.SwapToGame(
        LeaderboardDetail,
        true,
        UIStateManager.Manager.CurrentActivationKey);

    }

    public void OnDestroy()
    {
        UIStateManager.Manager.OnConnectionChange -= Manager_OnConnectionChange;
    }
}
