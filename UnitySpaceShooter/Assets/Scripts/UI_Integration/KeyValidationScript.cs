using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class KeyValidationScript : MonoBehaviour
{
    public InputField KeyField;
    public Text Message;
    public static KeyValiationStatesBack BackType;
    public static GameLevelDetail ParentLevelDetail;
    public static GameLevelDetail GrandParentLevelDetail;
    public static LeaderboardDetail LeaderboardDetail;
    float WaitingTime = 0.5f;
    string entryKey;
    bool LoadGame = false;
    string filePath = string.Empty;

    void Start()
    {

        LeaderboardStorage storage = UIStateManager.Manager.SettingsSystem.GetLeaderboardCache(UIStateManager.Manager.PlayerId, LeaderboardDetail.Id);
        if (storage != null)
        {
            if (storage.Leaderboard.Active)
            {
                UIStateManager.Manager.SwapToGame(storage.Leaderboard, true, storage.AccessKey);
            }
            else
            {
                UIStateManager.Manager.SwapToLeaderboardListPage(storage.Leaderboard, -1, storage.AccessKey);
            }
        }
    }

    public void DoKeyValidation()
    {
        string key = KeyField.text.Trim();
        UIStateManager.Manager.SetLoading(true);
        StartCoroutine(UIStateManager.GameAPI.ValidateParticipation(UIStateManager.Manager.PlayerId, key, LeaderboardDetail.Id, OnKeyValidation));
    }

    public void GetKey()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.GameAPI.OpenEntryKeyUrl(LeaderboardDetail.Id);

    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        BackNow();
    }

    private static void BackNow()
    {
        switch (BackType)
        {
            case KeyValiationStatesBack.LeaderBoards:
                UIStateManager.Manager.SwapToLeaderboardPage();
                break;

            case KeyValiationStatesBack.PostCreateLeaderboard:
                UIStateManager.Manager.SwapToPostCreateLeaderboardPage(LeaderboardDetail, ParentLevelDetail);
                break;

            case KeyValiationStatesBack.UserTournament:
                UIStateManager.Manager.SwapToUserTournamentsPage();
                break;

            case KeyValiationStatesBack.FromFav:
                UIStateManager.Manager.SwapToFavouritesPage();
                break;
            case KeyValiationStatesBack.MyTournaments:
                UIStateManager.Manager.SwapToSavedTournamentsPage();
                break;
            default:
                break;
        }
    }

    IEnumerator WaitAndLoadGame()
    {
        UIStateManager.Manager.SetLoading(true);
        yield return new WaitForSeconds(2);
        if (File.Exists(filePath))
        {
            UIStateManager.Manager.SwapToGame(LeaderboardDetail, true, entryKey);
        }
    }
    
    void OnGetGameLevelDetail(string errorString, GameLevelDetail result, params object[] userParam)
    {
        if (result != null && result.Active)
        {
            LeaderboardDetail leaderboardDetail = userParam[0] as LeaderboardDetail;
            LeaderboardDetail = leaderboardDetail;
            
            string key = KeyField.text.Trim();
            entryKey = key;
            string BannerFilePath = Path.Combine(Application.persistentDataPath, leaderboardDetail.Id + "_Banner.png");
            filePath = BannerFilePath;

            if (File.Exists(BannerFilePath))
            {
                UIStateManager.Manager.SwapToGame(LeaderboardDetail, true, entryKey);
            }

            if (leaderboardDetail.IsSponsored)
            {
                StartCoroutine(SetInterstitial(leaderboardDetail));
            }
        }
        else
        {
            Message.text = "Invalid key entered!";
        }
    }

    void OnGetLeaderboardDetail(string errorString, LeaderboardDetail result, params object[] userParam)
    {
        if (result != null && result.Active)
        {
            StartCoroutine(UIStateManager.GameAPI.GetGameLevelDetails(result.ParentLevelId, OnGetGameLevelDetail, result));
        }
        else
        {
            Message.text = "Invalid key entered!";
        }
    }

    void OnKeyValidation(string errorString, bool result, params object[] userParams)
    {
        Message.text = string.Empty;
        if (result)
        {
            //Update time stamp
            StartCoroutine(UIStateManager.GameAPI.GetLeaderboardDetails(LeaderboardDetail.Id, OnGetLeaderboardDetail));
        }
        else
        {
            Message.text = "Invalid key entered!";
        }
        UIStateManager.Manager.SetLoading(false);
    }

    IEnumerator SetInterstitial(LeaderboardDetail leaderboardDetail)
    {
        yield return null;
        UIStateManager.Manager.StartCoroutine
        (
            UIStateManager.GameAPI.GetBannerDetails
            (
                UIStateManager.Manager.PlayerId,
                leaderboardDetail.Id,
                OnGetBannerDetails
            )
        );
    }

    void OnGetBannerDetails(string errorString, BannerDetail result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString))
        {
            if (result != null)
            {
                string InterstitialSavePath = Path.Combine(Application.persistentDataPath, result.LeaderboardId + "_Interstitial.png");
                string BannerSavePath = Path.Combine(Application.persistentDataPath, result.LeaderboardId + "_Banner.png");
                string InterstitialUrlSaveName = result.LeaderboardId + "_Interstitial";
                string BannerUrlSaveName = result.LeaderboardId + "_Banner";

                if (!File.Exists(InterstitialSavePath))
                {
                    if (UIStateManager.Manager.GameOrientation == GameOrientation.Landscape)
                    {
                        Texture2D adTexture = new Texture2D(960, 640);
                        UIStateManager.Manager.StartCoroutine(UIStateManager.Manager.SetAdBanner(result.InterstitialLandscapeUrl, InterstitialUrlSaveName, adTexture, InterstitialSavePath, result.InterstitialClickUrl));
                    }
                    else if (UIStateManager.Manager.GameOrientation == GameOrientation.Portrait)
                    {
                        Texture2D adTexture = new Texture2D(640, 960);
                        UIStateManager.Manager.StartCoroutine(UIStateManager.Manager.SetAdBanner(result.InterstitialPotraitUrl, InterstitialUrlSaveName, adTexture, InterstitialSavePath, result.InterstitialClickUrl));
                    }
                    else
                    {
                        Texture2D adTexture = new Texture2D(960, 640);
                        UIStateManager.Manager.StartCoroutine(UIStateManager.Manager.SetAdBanner(result.InterstitialLandscapeUrl, InterstitialUrlSaveName, adTexture, InterstitialSavePath, result.InterstitialClickUrl));
                    }
                }
                if (!File.Exists(BannerSavePath))
                {
                    Texture2D BannerTexture = new Texture2D(320, 50);
                    UIStateManager.Manager.StartCoroutine(UIStateManager.Manager.SetAdBanner(result.BannerUrl, BannerUrlSaveName, BannerTexture, BannerSavePath, result.BannerClickUrl));
                }
                StartCoroutine(WaitAndLoadGame());
                UIStateManager.Manager.SetLoading(false);
            }
        }
    }
}
