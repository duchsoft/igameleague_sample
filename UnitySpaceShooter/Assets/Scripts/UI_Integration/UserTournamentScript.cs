using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserTournamentScript : MonoBehaviour
{
    public InputField tournamentID;
    public Text AdminPercentMessage;
    public Text Message;

    void Start()
    {
        StartCoroutine(UIStateManager.GameAPI.GetGameDetails(UIStateManager.Manager.GetGameID(), OnGetGameDetails));
    }

    void OnGetGameDetails(string errorString, GameDetail result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            AdminPercentMessage.text = "(Tournament creator gets " + result.LeaderboardAdminSharePercent.ToString() + "% of total points collected in the tournament)";
        }
    }

    void OnGetGameLevelDetail(string errorString, GameLevelDetail result, params object[] userParam)
    {
        if (result != null && result.Active)
        {
            LeaderboardDetail leaderboardDetail = userParam[0] as LeaderboardDetail;

            UIStateManager.Manager.SwapToKeyValidationPage(null, null, leaderboardDetail, KeyValiationStatesBack.UserTournament);
        }
    }

    void onMakeLeaderboardLive(string errorString, bool result, params object[] userParams)
    {
        if (string.IsNullOrEmpty(errorString) && result)
        {
            Debug.Log("Leaderboard is Live");
        }
        else
        {
            Debug.Log("We have a problem here");
        }
    }

    void OnGetLeaderboardDetails(string errorString, LeaderboardDetail[] result, params object[] userParam)
    {
        if (result != null && result.Length > 0 && result[0].Active)
        {
            StartCoroutine(UIStateManager.GameAPI.GetGameLevelDetails(result[0].ParentLevelId, OnGetGameLevelDetail, result));
            StartCoroutine(UIStateManager.GameAPI.MakeLeaderboardLive(
                leaderBoardId: result[0].Id,
                onMakeLeaderboardLive: onMakeLeaderboardLive));
        }
        else
        {
            Message.text = "Please enter a valid tournament ID";
        }
        UIStateManager.Manager.SetLoading(false);
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
                gameId: UIStateManager.Manager.GetGameID(),
                tag: tId,
                activeOnly: true,
                startIndex: 1,
                endIndex: 100,
                onGetLeaderboardDetail: OnGetLeaderboardDetails));
        }
    }
    public void SelectCreateTournament()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToCreateTournamentPage();
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToTournamentPage();
    }
}
