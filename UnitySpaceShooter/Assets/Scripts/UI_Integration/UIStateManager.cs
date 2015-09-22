using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class UIStateManager : MonoBehaviour
{
    [HideInInspector]
    public CurrencyType currencyType;
    public GameOrientation GameOrientation;
    [HideInInspector]
    public GameLevelSortType GameLevelSortType;
    public bool SandboxModeEnabled = false;
    public bool LockFps = true;
    public int FpsClampValue = 30;
    public bool ShowLoadingScreen = true;
    public bool DeleteAllPlayerPrefs = false;

    string LiveUrlFormat = "https://igameleague.azurewebsites.net/Service.asmx/{0}";

    //Live INR URLs

    string LiveRegistrationUrlInr = "https://www.igameleague.in/Registration.aspx";
    string LiveLeaderboardUrlInr = "https://www.igameleague.in/ScoreboardDetails.aspx";
    string LivePlayerPageUrlInr = "https://www.igameleague.in/PlayerZone/PlayerPage.aspx";
    string LiveEntryKeyUrlInr = "https://igameleague.azurewebsites.net/EntryKey.aspx";
    string BuylinkUrlInr = "https://www.igameleague.in/CommonZone/BuyPoints.aspx";
    string HomeUrlInr = "https://www.igameleague.in/default.aspx";

    //Live USD URLs

    string LiveSandboxRegistrationUrlUsd = "https://www.igameleague.com/Registration.aspx";
    string LiveLeaderboardUrlUsd = "https://www.igameleague.com/ScoreboardDetails.aspx";
    string LivePlayerPageUrlUsd = "https://www.igameleague.com/PlayerZone/PlayerPage.aspx";
    string LiveEntryKeyUrlUsd = "https://www.igameleague.com/EntryKey.aspx";
    string BuylinkUrlUsd = "https://igameleague-usd.azurewebsites.net/CommonZone/BuyPoints.aspx";
    string HomeUrlUsd = "https://www.igameleague.com/default.aspx";


    string SandboxUrlFormat = "http://sandbox-igameleague.azurewebsites.net/Service.asmx/{0}";

    //Sandbox INR URLs

    string SandboxRegistrationUrlInr = "https://sandbox-igameleague-inr.azurewebsites.net/Registration.aspx";
    string SandboxLeaderboardUrlInr = "https://sandbox-igameleague-inr.azurewebsites.net/ScoreboardDetails.aspx";
    string SandboxPlayerPageUrlInr = "https://sandbox-igameleague-inr.azurewebsites.net/PlayerZone/PlayerPage.aspx";
    string SandboxEntryKeyUrlInr = "https://sandbox-igameleague-inr.azurewebsites.net/EntryKey.aspx";
    [HideInInspector]
    public string SandboxBuylinkUrl = "https://sandbox-igameleague-inr.azurewebsites.net/CommonZone/BuyPoints.aspx";
    string SandboxHomeUrl = "https://sandbox-igameleague-inr.azurewebsites.net/default.aspx";

    //Sandbox USD URLs

    string SandboxRegistrationUrlUsd = "https://sandbox-igameleague.azurewebsites.net/Registration.aspx";
    string SandboxLeaderboardUrlUsd = "https://sandbox-igameleague.azurewebsites.net/ScoreboardDetails.aspx";
    string SandboxPlayerPageUrlUsd = "https://sandbox-igameleague.azurewebsites.net/PlayerZone/PlayerPage.aspx";
    string SandboxEntryKeyUrlUsd = "https://sandbox-igameleague.azurewebsites.net/EntryKey.aspx";
    [HideInInspector]
    public string SandboxBuylinkUrlUsd = "https://sandbox-igameleague.azurewebsites.net/CommonZone/BuyPoints.aspx";
    string SandboxHomeUrlUsd = "https://sandbox-igameleague.azurewebsites.net/default.aspx";

    //Common Live IDs

    public string DeveloperIdLive;
    public string IntegrationKeyLive;

    //Live INR IDs

    public string GameIdLiveInr;
    public string LiveTournamentIdLiveInr;
    public string UserTournamentIdLiveInr;

    //Live USD IDs

    public string GameIdLiveUsd;
    public string LiveTournamentIdLiveUsd;
    public string UserTournamentIdLiveUsd;

    //Common Sandbox IDs

    public string DeveloperIdSandbox;
    public string IntegrationKeySandbox;

    //Sandbox INR IDs

    public string GameIdSandboxInr;
    public string LiveTournamentIdSandboxInr;
    public string UserTournamentIdSandboxInr;

    //Sandbox USD IDs

    public string GameIdSandboxUsd;
    public string LiveTournamentIdSandboxUsd;
    public string UserTournamentIdSandboxUsd;

    public delegate void OnConnectionChangeHandler(bool online);
    public event OnConnectionChangeHandler OnConnectionChange;

    public static UIStateManager Manager;
    public static IGL GameAPI;
    public GameObject Loader;
    public SettingsSystem SettingsSystem;
    public ScoreHistory ScoreHistorySystem;
    public bool GameLoaded = false;

    public GameObject ListItemTemplate_LeaderboardTiles;
    public GameObject ListItemsTemplate_LeaderboardScores;
    public GameObject ListItemsTemplate_Buttons;
    public GameObject ListItemSelectionTemplate;
    public GameObject FavouritesToggleTemplate;
    public GameObject Alert;

    public Sprite FavTexture;
    public Sprite RemoveTexture;
    public Sprite ShareTexture;
    public Sprite SponsoredIcon;
    public Sprite UserTournamentIcon;

    public GameObject SplashTitlePage;
    public GameObject SplashCompanyLogoPage;
    public GameObject SplashPoweredLogoPage;
    public GameObject LoginPage;
    public GameObject MainMenuPage;
    public GameObject TournamentSelectionPage;
    public GameObject HelpPage;
    public GameObject ChallengesPage;
    public GameObject UserTournamentPage;
    public GameObject SubLevelPage;
    public GameObject LeaderboardPage;
    public GameObject KeyValidationPage;
    public GameObject LeaderboardListPage;
    public GameObject CreateTournamentPage;
    public GameObject PostCreateLeaderboardPage;
    public GameObject FavouritesPage;
    public GameObject SavedTournamentsPage;
    public GameObject LoadingPanel;

    GameObject PreviousInstance;
    GameObject PreviousLoader;
    GameObject LoadingScreen;

    public string GameName;
    public string GamePlaySceneName;
    public string MainMenuSceneName;
    public string GameDownloadLink;
    bool doAnimationPrevious = false;
    bool doAnimationCurrent = false;
    float fadeInDuration = 0.05f;
    float fadeOutDuration = 0.01f;
    GameObject currentObject = null;


    public LeaderboardDetail CurrentLeaderboardDetail;
    public bool CurrentGamePlayMode = false;
    public string CurrentActivationKey = string.Empty;

    public bool ShowScore = false;

    string gameDownloadUrl = string.Empty;
    string registrationUrl = string.Empty;
    string playerPageUrl = string.Empty;
    string leaderboardUrl = string.Empty;
    string entryKeyUrl = string.Empty;
    string buyUrl = string.Empty;
    string homeUrl = string.Empty;

    string developerId = string.Empty;
    string integrationKey = string.Empty;
    string urlFormat = string.Empty;
    string gameId = string.Empty;
    string liveTournamentId = string.Empty;
    string userTournamentId = string.Empty;

    void Awake()
    {
        if (LockFps)
        {
            Application.targetFrameRate = FpsClampValue;
        }

        if (Manager)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Manager = this;
        }
    }

    void DoAlertUser()
    {
        if (UIStateManager.Manager.SettingsSystem.PlayerScoreStorageSystem.Count > 0)
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            GameObject alertObject = GameObject.FindGameObjectWithTag("Alert");
            if (alertObject == null)
            {
                alertObject = GameObject.Instantiate(Alert);
            }

            alertObject.transform.SetParent(canvasObject.transform, false);
            alertObject.SetActive(true);
        }
        else
        {
            GameObject alertObject = GameObject.FindGameObjectWithTag("Alert");
            if (alertObject != null)
            {
                GameObject.DestroyImmediate(alertObject);
                alertObject = null;
            }
        }
    }

    void OnAuthentication(string errorString, bool result, params object[] userParams)
    {
        if (string.IsNullOrEmpty(errorString))
        {
            if (!result)
            {
                PlayerId = "";
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        if (DeleteAllPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        GameAPI = new IGL(GetDeveloperID(), GetIntegrationKey());

        IGL.URL_FORMAT = GetUrlFormat();
        IGL.REGISTRATION_URL = GetRegistrationUrl();
        IGL.LEADERBOARD_URL = GetLeaderboardUrl();
        IGL.PLAYER_PAGE_URL = GetPlayerPageUrl();
        IGL.ENTRYKEY_URL = GetEntryKeyUrl();
        Debug.Log(IGL.URL_FORMAT);
        Debug.Log(GetDeveloperID());
        switch (GameOrientation)
        {
            case GameOrientation.Landscape:
                break;
            case GameOrientation.Portrait:
                break;
        }

        switch (GameLevelSortType)
        {
            case GameLevelSortType.CreatedDate:
                break;
            case GameLevelSortType.JoiningPoint:
                break;
            case GameLevelSortType.Name:
                break;
        }

        Manager.SettingsSystem = SettingsSystem.Load();
        Manager.ScoreHistorySystem = ScoreHistory.Load();
        if (Manager.SettingsSystem == null)
        {
            Manager.SettingsSystem = new SettingsSystem();
        }
        else
        {
            SettingsSystem.ProcessLeaderboardStorageValidation();
        }

        if (Manager.ScoreHistorySystem == null)
        {
            Manager.ScoreHistorySystem = new ScoreHistory();
        }
        else
        {
            ScoreHistorySystem.ProcessLeaderboardStorageValidation();
        }

        if (!String.IsNullOrEmpty(PlayerId))
        {
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(PlayerId, OnAuthentication));
        }

        StartProcessScoreQueue();
        SwapToCompanyLogoSplash();
    }

    public string GetDeveloperID()
    {
        if (SandboxModeEnabled)
        {
            developerId = DeveloperIdSandbox;
        }
        else
        {
            developerId = DeveloperIdLive;
        }

        return developerId;
    }

    public string GetIntegrationKey()
    {
        if (SandboxModeEnabled)
        {
            integrationKey = IntegrationKeySandbox;
        }
        else
        {
            integrationKey = IntegrationKeyLive;
        }

        return integrationKey;
    }

    public string GetUrlFormat()
    {
        if (SandboxModeEnabled)
        {
            urlFormat = SandboxUrlFormat;
            Debug.Log(urlFormat);
        }
        else
        {
            urlFormat = LiveUrlFormat;
        }

        return urlFormat;
    }

    public string GetGameID()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    gameId = GameIdSandboxInr;
                    break;
                case CurrencyType.USD:
                    gameId = GameIdSandboxUsd;
                    break;
            }

        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    gameId = GameIdLiveInr;
                    break;
                case CurrencyType.USD:
                    gameId = GameIdLiveUsd;
                    break;
            }
        }
        return gameId;
    }

    public string GetLiveTournamentID()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    liveTournamentId = LiveTournamentIdSandboxInr;
                    break;
                case CurrencyType.USD:
                    liveTournamentId = LiveTournamentIdSandboxUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    liveTournamentId = LiveTournamentIdLiveInr;
                    break;
                case CurrencyType.USD:
                    liveTournamentId = LiveTournamentIdLiveUsd;
                    break;
            }
        }

        return liveTournamentId;
    }

    public string GetUserTournamentID()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    userTournamentId = UserTournamentIdSandboxInr;
                    break;
                case CurrencyType.USD:
                    userTournamentId = UserTournamentIdSandboxUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    userTournamentId = UserTournamentIdLiveInr;
                    break;
                case CurrencyType.USD:
                    userTournamentId = UserTournamentIdLiveUsd;
                    break;
            }
        }

        return userTournamentId;
    }

    public string GetRegistrationUrl()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    registrationUrl = SandboxRegistrationUrlInr;
                    break;
                case CurrencyType.USD:
                    registrationUrl = SandboxRegistrationUrlUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    registrationUrl = LiveRegistrationUrlInr;
                    break;
                case CurrencyType.USD:
                    registrationUrl = LiveSandboxRegistrationUrlUsd;
                    break;
            }
        }

        return registrationUrl;
    }

    public string GetLeaderboardUrl()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    leaderboardUrl = SandboxLeaderboardUrlInr;
                    break;
                case CurrencyType.USD:
                    leaderboardUrl = SandboxLeaderboardUrlUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    leaderboardUrl = LiveLeaderboardUrlInr;
                    break;
                case CurrencyType.USD:
                    leaderboardUrl = LiveLeaderboardUrlUsd;
                    break;
            }
        }

        return leaderboardUrl;
    }

    public string GetPlayerPageUrl()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    playerPageUrl = SandboxPlayerPageUrlInr;
                    break;
                case CurrencyType.USD:
                    playerPageUrl = SandboxPlayerPageUrlUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    playerPageUrl = LivePlayerPageUrlInr;
                    break;
                case CurrencyType.USD:
                    playerPageUrl = LivePlayerPageUrlUsd;
                    break;
            }
        }

        return playerPageUrl;
    }

    public string GetEntryKeyUrl()
    {
        if (SandboxModeEnabled)
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    entryKeyUrl = SandboxEntryKeyUrlInr;
                    break;
                case CurrencyType.USD:
                    entryKeyUrl = SandboxEntryKeyUrlUsd;
                    break;
            }
        }
        else
        {
            switch (currencyType)
            {
                case CurrencyType.INR:
                    entryKeyUrl = LiveEntryKeyUrlInr;
                    break;
                case CurrencyType.USD:
                    entryKeyUrl = LiveEntryKeyUrlUsd;
                    break;
            }
        }

        return entryKeyUrl;
    }

    public void SetCurrencyType(int currencyType)
    {
        PlayerPrefs.SetInt("CurrencyType", currencyType);
    }

    public void GetCurrency()
    {
        if (PlayerPrefs.HasKey("CurrencyType"))
        {
            currencyType = (CurrencyType)PlayerPrefs.GetInt("CurrencyType");
        }
    }

    private void StartProcessScoreQueue()
    {
        InvokeRepeating("ProcessScoreQue", 5, 10);
    }

    void CheckConnection(string errorString, bool result, params object[] userParams)
    {
        if (OnConnectionChange != null)
        {
            OnConnectionChange(string.IsNullOrEmpty(errorString));
        }
    }

    void ProcessScoreQue()
    {
        if (!String.IsNullOrEmpty(PlayerId))
        {
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(PlayerId, CheckConnection));
        }

        StartCoroutine(UIStateManager.Manager.SettingsSystem.ProcessScoreQue());
    }

    void Update()
    {
        if (doAnimationPrevious)
        {
            if (PreviousInstance != null && PreviousInstance.activeInHierarchy)
            {
                CanvasGroup group = PreviousInstance.GetComponent<CanvasGroup>();
                if (group != null)
                {
                    float val = group.alpha - (0.1f * (Time.smoothDeltaTime / fadeOutDuration));
                    if (val < 0)
                    {
                        val = 0;
                    }

                    group.alpha = val;
                    if (group.alpha <= 0)
                    {
                        GameObject.Destroy(PreviousInstance);
                        PreviousInstance = currentObject;
                        doAnimationPrevious = false;
                        doAnimationCurrent = true;
                    }
                }
            }
        }
        if (doAnimationCurrent)
        {
            if (currentObject != null)
            {
                CanvasGroup group = currentObject.GetComponent<CanvasGroup>();
                if (group != null)
                {
                    if (currentObject.activeInHierarchy)
                    {
                        group.alpha = Math.Min(group.alpha + (0.1f * (Time.smoothDeltaTime / fadeInDuration)), 1);
                        if (group.alpha >= 1)
                        {
                            doAnimationCurrent = false;
                            PreviousInstance = currentObject;
                            currentObject = null;
                        }
                    }
                    else
                    {
                        currentObject.SetActive(true);
                        group.alpha = 0;
                    }
                }
            }
        }
    }

    void SwapToPage(GameObject obj)
    {
        currentObject = GameObject.Instantiate(obj);
        GameObject canvas = GameObject.Find("Canvas");
        currentObject.transform.SetParent(canvas.transform, false);
        currentObject.SetActive(false);


        if (PreviousInstance == null)
        {
            doAnimationCurrent = true;
        }
        else
        {
            doAnimationPrevious = true;
        }

        if (obj != SplashCompanyLogoPage && obj != SplashTitlePage && obj != SplashPoweredLogoPage)
        {
            DoAlertUser();
        }
        if (GameLoaded)
        {
            GameLoaded = false;
            StartProcessScoreQueue();
        }
    }

    public void SwapToTitleSplash()
    {
        SwapToPage(SplashTitlePage);
    }

    public void SwapToCompanyLogoSplash()
    {
        SwapToPage(SplashCompanyLogoPage);
    }

    public void SwapToPoweredLogoSplash()
    {
        SwapToPage(SplashPoweredLogoPage);
    }

    public void SwapToLogin(bool force)
    {
        if (force || string.IsNullOrEmpty(PlayerId))
        {
            SwapToPage(LoginPage);
        }
        else
        {
            SwapToTournamentPage();
        }

    }

    public void SwapToMainMenu()
    {
        SwapToPage(MainMenuPage);
    }

    public void SwapToSavedTournamentsPage()
    {
        SwapToPage(SavedTournamentsPage);
    }

    public void SwapToTournamentPage()
    {
        SwapToPage(TournamentSelectionPage);
    }

    public void SwapToHelpPage()
    {
        SwapToPage(HelpPage);
    }


    public void OpenMoreGames()
    {
        Application.OpenURL("http://games4stars.azurewebsites.net/Games.aspx");
    }

    public void SwapToChallengesPage()
    {
        SwapToPage(ChallengesPage);
    }

    void OnAuthenticationUserTournament(string errorString, bool result, params object[] userParams)
    {
        if (string.IsNullOrEmpty(errorString))
        {
            SwapToPage(UserTournamentPage);
        }
        else
        {
            SwapToSavedTournamentsPage();
        }
    }

    public void SwapToUserTournamentsPage()
    {
        if (!String.IsNullOrEmpty(UIStateManager.Manager.PlayerId))
        {
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(PlayerId, OnAuthenticationUserTournament));
        }
    }

    public void SwapToFavouritesPage()
    {
        SwapToPage(FavouritesPage);
    }

    public void SwapToSubLevelPage(GameLevelDetail levelDetail)
    {
        SubLevelsScript.Level = levelDetail;
        SwapToPage(SubLevelPage);
    }

    public void SwapToLeaderboardPage()
    {
        SwapToPage(LeaderboardPage);
    }

    void OnUpdateScore(string errorString, bool result, params object[] userParams)
    {
        LeaderboardListScript.LeaderboardDetail = userParams[0] as LeaderboardDetail;
        LeaderboardListScript.Score = (double)userParams[1];
        string activitaionKey = userParams[2].ToString();
        LeaderboardListScript.GotErrorDuringScoreUpdate = false;
        if (!string.IsNullOrEmpty(errorString))
        {
            SettingsSystem.AddToProcessQue(LeaderboardListScript.LeaderboardDetail, activitaionKey, LeaderboardListScript.Score);
            LeaderboardListScript.GotErrorDuringScoreUpdate = true;
        }
        SwapToPage(LeaderboardListPage);
    }

    void OnAuthenticationDuringLeaderboardList(string errorString, bool result, params object[] userParams)
    {
        LeaderboardListScript.LeaderboardDetail = userParams[0] as LeaderboardDetail;
        LeaderboardListScript.Score = (double)userParams[1];
        LeaderboardListScript.GotErrorDuringScoreUpdate = !string.IsNullOrEmpty(errorString);
        SwapToPage(LeaderboardListPage);
    }

    public void SwapToLeaderboardListPage(LeaderboardDetail leaderboardDetail, double score, string activationKey)
    {
        if (score == -1)
        {
            ShowScore = false;
            StartCoroutine(GameAPI.IsActivePlayer(PlayerId, OnAuthenticationDuringLeaderboardList, leaderboardDetail, score));
        }
        else
        {
            ShowScore = true;
            StartCoroutine(GameAPI.UpdatePlayerScore(
                userName: PlayerId,
                participationKey: activationKey,
                leaderBoardId: leaderboardDetail.Id,
                score: score,
                updateIfBetterThanPrevious: true,
                onUpdatePlayerScoreFn: OnUpdateScore));
            ScoreHistorySystem.AddEntry(PlayerId, score, leaderboardDetail.Id, DateTime.Now.ToString());
        }
    }

    public List<ScoreHistoryItem> GetUserScoreHistory(LeaderboardDetail detail)
    {
        return ScoreHistorySystem.GetEntries(PlayerId, detail);
    }

    public void SwapToKeyValidationPage(
    GameLevelDetail grandParentLevelDetail,
    GameLevelDetail parentLevelDetail,
    LeaderboardDetail leaderboardDetail,
    KeyValiationStatesBack backType)
    {
        KeyValidationScript.BackType = backType;
        KeyValidationScript.GrandParentLevelDetail = grandParentLevelDetail;
        KeyValidationScript.ParentLevelDetail = parentLevelDetail;
        KeyValidationScript.LeaderboardDetail = leaderboardDetail;
        SwapToPage(KeyValidationPage);
    }

    public void SwapToCreateTournamentPage()
    {
        SwapToPage(CreateTournamentPage);
    }

    public void SwapToPostCreateLeaderboardPage(LeaderboardDetail leaderboardDetail, GameLevelDetail levelDetail)
    {
        PostCreateLeaderboardScript.LeaderboardDetail = leaderboardDetail;
        PostCreateLeaderboardScript.LevelDetail = levelDetail;
        SwapToPage(PostCreateLeaderboardPage);
    }

    void ShowLoadingPanel()
    {
        if (ShowLoadingScreen)
        {
            if (Application.isLoadingLevel)
            {
                LoadingScreen = GameObject.Instantiate(LoadingPanel);
            }
            else
            {
                if (LoadingScreen != null)
                {
                    Destroy(LoadingScreen);
                }
            }
        }
    }

    public void SwapToGame(LeaderboardDetail leaderboardDetail, bool isGamePlayMode, string activationKey)
    {
        this.CurrentLeaderboardDetail = leaderboardDetail;
        this.CurrentGamePlayMode = isGamePlayMode;
        this.CurrentActivationKey = activationKey;
        string playName = string.Empty;
        if (leaderboardDetail != null)
        {
            SplitExtras(leaderboardDetail.Extras);
        }
        CancelInvoke("ProcessScoreQue");
        GameLoaded = true;
        if (!string.IsNullOrEmpty(playName))
        {
            Application.LoadLevel(playName);
            ShowLoadingPanel();
        }
        else
        {
            Application.LoadLevel(GamePlaySceneName);
            ShowLoadingPanel();

        }

    }

    public void LoadMenuScene(bool loadMainMenu)
    {
        Application.LoadLevel(MainMenuSceneName);
        ShowLoadingPanel();
        if (loadMainMenu)
        {
            StartCoroutine(LoadMainPage());
        }
    }

    IEnumerator LoadMainPage()
    {
        yield return null;
        SwapToMainMenu();
    }

    public string PlayerId
    {
        get
        {
            return PlayerPrefs.GetString("xxxxx12121");
        }
        set
        {
            PlayerPrefs.SetString("xxxxx12121", value);
        }
    }

    public void SetLoading(bool val)
    {
        if (val)
        {
            if (PreviousLoader != null && PreviousLoader.activeInHierarchy)
            {
                GameObject.Destroy(PreviousLoader);
            }

            PreviousLoader = GameObject.Instantiate(Loader);

            GameObject canvas = GameObject.Find("Canvas");

            PreviousLoader.transform.SetParent(canvas.transform, false);
        }
        else
        {
            if (PreviousLoader != null && PreviousLoader.activeInHierarchy)
            {
                GameObject.Destroy(PreviousLoader);
            }
        }
    }

    public IEnumerator SetBannerImage(string url, RawImage image)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            if (image != null)
                image.texture = www.texture;
        }
    }

    public IEnumerator SetAdBanner(string url, string urlSaveName, Texture2D image, string savePath, string clickUrl)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            if (image != null && www.texture != null)
            {
                image = www.texture;
                byte[] dataToSave = image.EncodeToPNG();
                File.WriteAllBytes(savePath, dataToSave);
                PlayerPrefs.SetString(urlSaveName, clickUrl);
            }
        }
    }

    public string GetLeaderboardData(GameLevelDetail detail, LeaderboardDetail leaderboardDetail)
    {
        if (!string.IsNullOrEmpty(leaderboardDetail.Extras))
        {
            return SplitExtras(leaderboardDetail.Extras);
        }
        return SplitExtras(detail.Extras);
    }

    public string SplitExtras(string extras)
    {
        if (!string.IsNullOrEmpty(extras))
        {
            string[] tokens = extras.Split(',');

            if (tokens.Length == 1)
            {
                return tokens[0];
            }
        }

        return string.Empty;
    }


    public void SetLeaderboardName(LeaderboardDetail item, Button objButton)
    {
        DateTime localTime = GetLeaderboardValidity(item, item.LifeTime / 60);
        Text objText = objButton.GetComponentInChildren<Text>();
        GameObject sponsoredFlag = objButton.transform.FindChild("Image").gameObject;
        objText.text = string.Format("{0}\nJoining Point: {1}, Reward Points: {2}\nValid till: {3}",
                 item.Name,
                 item.JoiningPoint,
                 item.TotalRewardPoints > 0 ? string.Format("{0}+", item.TotalRewardPoints.ToString()) : "0",
                 localTime);


        if (item.IsSponsored)
        {
            sponsoredFlag.SetActive(true);
            sponsoredFlag.GetComponent<Image>().sprite = SponsoredIcon;
        }
        else if (item.InviteOnly)
        {
            sponsoredFlag.SetActive(true);
            sponsoredFlag.GetComponent<Image>().sprite = UserTournamentIcon;
        }
        else
        {
            sponsoredFlag.SetActive(false);
        }



        //objText.text += string.Format("\nValid upto : {0}", localTime);
    }

    public bool isLeaderboardExpired(LeaderboardDetail item)
    {
        DateTime leaderboardExpiration = DateTime.Parse(item.LiveTime).AddMinutes((item.LifeTime / 60));
        DateTime localTime = TimeZone.CurrentTimeZone.ToLocalTime(leaderboardExpiration);
        return localTime <= DateTime.Now;
    }

    public DateTime GetLeaderboardValidity(LeaderboardDetail item, double lifeTime)
    {
        DateTime leaderboardExpiration = DateTime.Parse(item.LiveTime).AddMinutes(lifeTime);
        DateTime localTime = TimeZone.CurrentTimeZone.ToLocalTime(leaderboardExpiration);
        return localTime;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}

[Serializable]
public class PlayerScoreStorage
{
    public string PlayerId = string.Empty;
    public string AccessKey = string.Empty;
    public LeaderboardDetail Leaderboard = null;
    public double Score = 0;
}

[Serializable]
public class LeaderboardStorage
{
    public string PlayerId = string.Empty;
    public string AccessKey = string.Empty;
    public LeaderboardDetail Leaderboard = null;
}


[Serializable]
public class SettingsSystem
{
    public List<PlayerScoreStorage> PlayerScoreStorageSystem = new List<PlayerScoreStorage>();
    public List<LeaderboardStorage> LeaderboardStorageSystem = new List<LeaderboardStorage>();
    public List<LeaderboardStorage> FavStorageSystem = new List<LeaderboardStorage>();

    void OnUpdateScore(string errorString, bool result, params object[] userParams)
    {
        PlayerScoreStorage storage = userParams[0] as PlayerScoreStorage;
        if (!string.IsNullOrEmpty(errorString))
        {
            PlayerScoreStorageSystem.Add(storage);
            Save();
        }
        else
        {
        }
    }

    void OnGetServerTime(string errorString, string result, params object[] userParam)
    {
        bool isFav = (bool)userParam[0];
        LeaderboardStorage item = userParam[1] as LeaderboardStorage;
        if (string.IsNullOrEmpty(errorString))
        {
            DateTime timeNow = DateTime.Parse(result);
            DateTime leaderboardExpiration = DateTime.Parse(item.Leaderboard.LiveTime).AddSeconds(item.Leaderboard.LifeTime + 86400);

            if (item.Leaderboard.Active || leaderboardExpiration > timeNow)
            {
                if (isFav)
                {
                    FavStorageSystem.Add(item);
                }
                else
                {
                    LeaderboardStorageSystem.Add(item);
                }
                Save();
            }
            else
            {
                //find any banner releated data if found delte dor this leaderboard
                string id = item.Leaderboard.Id;
                string InterstitialSavePath = Path.Combine(Application.persistentDataPath, id + "_Interstitial.png");
                string BannerSavePath = Path.Combine(Application.persistentDataPath, id + "_Banner.png");
                string InterstitialUrlSaveName = id + "_Interstitial";
                string BannerUrlSaveName = id + "_Banner";

                if (!string.IsNullOrEmpty(InterstitialSavePath))
                {
                    File.Delete(InterstitialSavePath);
                }

                if (!string.IsNullOrEmpty(BannerSavePath))
                {
                    File.Delete(BannerSavePath);
                }

                if (PlayerPrefs.HasKey(InterstitialUrlSaveName))
                {
                    PlayerPrefs.DeleteKey(InterstitialUrlSaveName);
                }

                if (PlayerPrefs.HasKey(BannerUrlSaveName))
                {
                    PlayerPrefs.DeleteKey(BannerUrlSaveName);
                }
            }
        }
        else
        {
            if (isFav)
            {
                FavStorageSystem.Add(item);
            }
            else
            {
                LeaderboardStorageSystem.Add(item);
            }
            Save();
        }
    }

    void OnGetLeaderboardDetail(string errorString, LeaderboardDetail result, params object[] userParam)
    {
        bool isFav = (bool)userParam[1];

        LeaderboardStorage item = userParam[0] as LeaderboardStorage;
        if (string.IsNullOrEmpty(errorString))
        {
            if (result != null)
            {
                item.Leaderboard = result;
                UIStateManager.Manager.StartCoroutine(UIStateManager.GameAPI.GetCurrentTime(
                    OnGetServerTime,
                    isFav,
                    item));
            }
        }
        else
        {
            if (isFav)
            {
                FavStorageSystem.Add(item);
            }
            else
            {
                LeaderboardStorageSystem.Add(item);
            }
            Save();
        }
    }

    public void ProcessLeaderboardStorageValidation()
    {
        List<LeaderboardStorage> LeaderboardStorageSystemCopy = new List<LeaderboardStorage>(LeaderboardStorageSystem.ToArray());
        LeaderboardStorageSystem.Clear();

        foreach (LeaderboardStorage item in LeaderboardStorageSystemCopy)
        {
            UIStateManager.Manager.StartCoroutine(
                UIStateManager.GameAPI.GetLeaderboardDetails(item.Leaderboard.Id, OnGetLeaderboardDetail, item, false));
        }

        LeaderboardStorageSystemCopy = new List<LeaderboardStorage>(FavStorageSystem.ToArray());
        FavStorageSystem.Clear();
        foreach (LeaderboardStorage item in LeaderboardStorageSystemCopy)
        {
            UIStateManager.Manager.StartCoroutine(
                UIStateManager.GameAPI.GetLeaderboardDetails(item.Leaderboard.Id, OnGetLeaderboardDetail, item, true));
        }
        Save();
    }

    public IEnumerator ProcessScoreQue()
    {
        List<PlayerScoreStorage> PlayerScoreStorageSystemCopy = new List<PlayerScoreStorage>(PlayerScoreStorageSystem.ToArray());
        PlayerScoreStorageSystem.Clear();

        foreach (PlayerScoreStorage item in PlayerScoreStorageSystemCopy)
        {
            UIStateManager.Manager.StartCoroutine(UIStateManager.GameAPI.UpdatePlayerScore(
                userName: item.PlayerId,
                participationKey: item.AccessKey,
                leaderBoardId: item.Leaderboard.Id,
                score: item.Score,
                updateIfBetterThanPrevious: true,
                onUpdatePlayerScoreFn: OnUpdateScore));
            yield return null;
        }
    }

    public void AddToProcessQue(LeaderboardDetail detail, string accessKey, double score)
    {
        if (detail.Active)
        {
            PlayerScoreStorage store = new PlayerScoreStorage();
            store.Leaderboard = detail;
            store.PlayerId = UIStateManager.Manager.PlayerId;
            store.AccessKey = accessKey;
            store.Score = score;
            PlayerScoreStorageSystem.Add(store);
            Save();
        }
    }

    public LeaderboardStorage GetLeaderboardCache(string playerId, string leaderboardId)
    {
        foreach (LeaderboardStorage item in LeaderboardStorageSystem)
        {
            if (item.PlayerId == playerId && item.Leaderboard.Id == leaderboardId)
            {
                return item;
            }
        }

        return null;
    }

    public LeaderboardStorage AddToLeaderboardCache(string playerId, LeaderboardDetail detail, string accessKey, bool fav)
    {
        LeaderboardStorage item = GetLeaderboardCache(playerId, detail.Id);
        if (item != null)
        {
            if (fav)
            {
                FavStorageSystem.Remove(item);
            }
            else
            {
                LeaderboardStorageSystem.Remove(item);
            }

        }
        item = new LeaderboardStorage();
        item.Leaderboard = detail;
        item.PlayerId = playerId;
        item.AccessKey = accessKey;
        if (fav)
        {
            FavStorageSystem.Add(item);
        }
        else
        {
            LeaderboardStorageSystem.Add(item);
        }
        Save();
        return item;
    }

    public LeaderboardStorage[] GetLeaderboardCache(string playerId)
    {
        List<LeaderboardStorage> storage = new List<LeaderboardStorage>();
        foreach (LeaderboardStorage item in LeaderboardStorageSystem)
        {
            if (item.PlayerId == playerId)
            {
                storage.Add(item);
            }
        }
        storage.Reverse();
        return storage.ToArray();
    }

    public LeaderboardStorage[] GetFavs(string playerId)
    {
        List<LeaderboardStorage> storage = new List<LeaderboardStorage>();
        foreach (LeaderboardStorage item in FavStorageSystem)
        {
            if (item.PlayerId == playerId)
            {
                storage.Add(item);
            }
        }
        storage.Reverse();
        return storage.ToArray();
    }

    public void RemoveFromFav(string playerId, string leaderboardId)
    {
        List<LeaderboardStorage> storage = new List<LeaderboardStorage>();
        foreach (LeaderboardStorage item in FavStorageSystem)
        {
            if (item.PlayerId == playerId && item.Leaderboard.Id == leaderboardId)
            {
                storage.Add(item);
            }
        }
        foreach (LeaderboardStorage item in storage)
        {
            FavStorageSystem.Remove(item);
        }
        Save();
    }

    const string key = "xxxxx12331";
    void Save()
    {
        StreamWriter stWriter = null;
        XmlSerializer xmlSerializer;
        string buffer;
        try
        {
            xmlSerializer = new XmlSerializer(this.GetType());
            MemoryStream memStream = new MemoryStream();
            stWriter = new StreamWriter(memStream);
            xmlSerializer.Serialize(stWriter, this);
            buffer = Encoding.ASCII.GetString(memStream.GetBuffer());
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally
        {
            if (stWriter != null)
                stWriter.Close();
        }
        PlayerPrefs.SetString(key, buffer);
    }

    public static SettingsSystem Load()
    {
        try
        {
            string buffer = PlayerPrefs.GetString(key);
            TextReader stReader = null;
            XmlSerializer xmlSerializer;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(SettingsSystem));
                stReader = new StringReader(buffer);
                return xmlSerializer.Deserialize(stReader) as SettingsSystem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (stReader != null)
                    stReader.Close();
            }


        }
        catch (Exception)
        {

        }

        return null;

    }
}

public class ScoreHistoryItem
{
    public string UserId
    {
        get;
        set;
    }

    public double Score
    {
        get;
        set;
    }

    public string LeaderboardId
    {
        get;
        set;
    }

    public string TimeStamp
    {
        get;
        set;
    }
}

public class ScoreHistory
{
    const int MAXCOUNT = 100;

    const string key = "xxxxx5552224";

    public List<ScoreHistoryItem> Items = new List<ScoreHistoryItem>();

    void Save()
    {
        StreamWriter stWriter = null;
        XmlSerializer xmlSerializer;
        string buffer;
        try
        {
            xmlSerializer = new XmlSerializer(this.GetType());
            MemoryStream memStream = new MemoryStream();
            stWriter = new StreamWriter(memStream);
            xmlSerializer.Serialize(stWriter, this);
            buffer = Encoding.ASCII.GetString(memStream.GetBuffer());
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally
        {
            if (stWriter != null)
                stWriter.Close();
        }
        PlayerPrefs.SetString(key, buffer);
    }

    public static ScoreHistory Load()
    {
        try
        {
            string buffer = PlayerPrefs.GetString(key);
            TextReader stReader = null;
            XmlSerializer xmlSerializer;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(ScoreHistory));
                stReader = new StringReader(buffer);
                return xmlSerializer.Deserialize(stReader) as ScoreHistory;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (stReader != null)
                    stReader.Close();
            }


        }
        catch (Exception)
        {

        }

        return null;

    }

    public List<ScoreHistoryItem> GetEntries(string userId, LeaderboardDetail detail)
    {
        List<ScoreHistoryItem> historyList = new List<ScoreHistoryItem>();
        foreach (ScoreHistoryItem item in Items)
        {
            if (userId == item.UserId && item.LeaderboardId == detail.Id)
            {
                historyList.Add(item);
            }
        }
        return historyList;
    }

    public double GetHighScore(string userId, LeaderboardDetail detail)
    {
        double score = 0;

        foreach (ScoreHistoryItem item in Items)
        {
            if (userId == item.UserId && item.LeaderboardId == detail.Id)
            {
                if (score < item.Score)
                {
                    score = item.Score;
                }
            }
        }

        return score;
    }

    void OnGetLeaderboardDetail(string errorString, LeaderboardDetail result, params object[] userParam)
    {
        ScoreHistoryItem item = userParam[0] as ScoreHistoryItem;
        if (string.IsNullOrEmpty(errorString))
        {
            if (result != null && result.Active)
            {
                Items.Add(item);
                Save();
            }
        }
        else
        {
            Items.Add(item);
            Save();
        }
    }

    public void ProcessLeaderboardStorageValidation()
    {
        List<ScoreHistoryItem> LeaderboardStorageSystemCopy = new List<ScoreHistoryItem>(Items.ToArray());
        LeaderboardStorageSystemCopy.Clear();

        foreach (ScoreHistoryItem item in LeaderboardStorageSystemCopy)
        {
            UIStateManager.Manager.StartCoroutine(
                UIStateManager.GameAPI.GetLeaderboardDetails(item.LeaderboardId, OnGetLeaderboardDetail, item));
        }
        Save();
    }

    public void AddEntry(string userId, double score, string leaderboardId, string timeStamp)
    {
        ScoreHistoryItem item = new ScoreHistoryItem();
        item.LeaderboardId = leaderboardId;
        item.Score = score;
        item.TimeStamp = timeStamp;
        item.UserId = userId;
        Items.Add(item);
        Save();
    }
}

public enum KeyValiationStatesBack
{
    LeaderBoards,
    PostCreateLeaderboard,
    UserTournament,
    FromFav,
    MyTournaments
}
