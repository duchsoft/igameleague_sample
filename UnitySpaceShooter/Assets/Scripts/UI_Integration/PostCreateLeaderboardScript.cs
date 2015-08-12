using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PostCreateLeaderboardScript : MonoBehaviour
{
	public static LeaderboardDetail LeaderboardDetail;
	public static GameLevelDetail LevelDetail;
	public InputField LeaderboardIdField;

	public Image OthersImage;
	public Sprite MailIcon;
	public Sprite ShareIcon;

	public Text TournamentValidityLabel;

	public Text logText;

	// Use this for initialization
	void Start ()
	{
		LeaderboardIdField.text = LeaderboardDetail.Tag;

        TournamentValidityLabel.text = UIStateManager.Manager.GetLeaderboardValidity(LeaderboardDetail, LeaderboardDetail.LifeTime / 60).ToString();



#if UNITY_ANDROID || UNITY_IPHONE
		OthersImage.sprite = ShareIcon;
#else 
		OthersImage.sprite = MailIcon;
#endif
        
	}

	public void InviteFriends ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
#if UNITY_ANDROID

        new OtherShareUtilsAndroid().DoInviteFriends(LeaderboardDetail.Tag, TournamentValidityLabel.text, LeaderboardDetail.JoiningPoint);

#elif UNITY_IPHONE

		new OtherShareUtilsIOS (gameObject.GetComponent<Diffusion> ()).DoInviteFriends (LeaderboardDetail.Tag, TournamentValidityLabel.text, LeaderboardDetail.JoiningPoint);
#else

        Application.OpenURL("mailto:" + "" + "?subject=" + "Let's play " + UIStateManager.Manager.GameName + "&body=" + BaseInviteFrineds.GetInviteMessage(LeaderboardDetail.Tag, TournamentValidityLabel.text, LeaderboardDetail.JoiningPoint));

#endif
    }

	void OnGetGameLevelDetail (string errorString, GameLevelDetail result, params object[] userParam)
	{
		if (result != null && result.Active) {            
			LeaderboardDetail leaderboardDetail = userParam [0] as LeaderboardDetail;

			UIStateManager.Manager.SwapToKeyValidationPage (null, LevelDetail, leaderboardDetail, KeyValiationStatesBack.PostCreateLeaderboard);
		}
	}


	void OnGetLeaderboardsDetail (string errorString, LeaderboardDetail[] result, params object[] userParam)
	{
        if (result != null && result.Length > 0 && result[0].Active)
        {
			StartCoroutine (UIStateManager.GameAPI.GetGameLevelDetails (result[0].ParentLevelId, OnGetGameLevelDetail, result));
		}
		UIStateManager.Manager.SetLoading (false);
	}

	public void SelectJoinTournament ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		string tId = LeaderboardIdField.text.Trim ();
		if (!string.IsNullOrEmpty (tId)) {
			UIStateManager.Manager.SetLoading (true);
            StartCoroutine(UIStateManager.GameAPI.GetLeaderboardsByTagFromGame(
                userName: UIStateManager.Manager.PlayerId,
                gameId: UIStateManager.Manager.GameID,
                tag: tId,
                activeOnly: true,
                startIndex: 1,
                endIndex: 100,
                onGetLeaderboardDetail: OnGetLeaderboardsDetail));
		}
	}

	public void SelectMainMenu ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToMainMenu ();
	}

}

public abstract class BaseInviteFrineds
{
	public void DoInviteFriends (string leaderboardName, string validity, long joiningPoint)
	{
        string message = GetInviteMessage(leaderboardName, validity, joiningPoint);
		InviteFriends (message);
	}

    public static string GetInviteMessage(string leaderboardName, string validity, long joiningPoint)
	{
		string m_GameName = UIStateManager.Manager.GameName;

		string message = string.Format (
                                @"Hi,
                                 Win glory / prize in my tournament for {0}.To play, please download the game from {1}. To join tournament, please click Tournaments, then click User Tournaments in the game and use this tournament ID - {2}. It's valid till {3} ({4}). Joining points = {5}",
                                m_GameName,
                                UIStateManager.Manager.GameDownloadLink,
                                leaderboardName,
                                validity,
                                System.TimeZone.CurrentTimeZone.StandardName,
                                joiningPoint == 0 ? "0" : joiningPoint.ToString());
		return message;
	}

	protected abstract void InviteFriends (string message);
}

#if UNITY_ANDROID
public class OtherShareUtilsAndroid : BaseInviteFrineds
{
	static AndroidJavaClass sharePluginClass;

	protected override void InviteFriends (string message)
	{
		sharePluginClass.CallStatic ("share", new object[] {
						"Play " + UIStateManager.Manager.GameName,
						"",
                        message
				});
	}

	static OtherShareUtilsAndroid ()
	{
		sharePluginClass = new AndroidJavaClass ("com.ari.tool.UnityAndroidTool");
		if (sharePluginClass == null) {
			Debug.Log ("sharePluginClass is null");
		} else {
			Debug.Log ("sharePluginClass is not null");
		}
	}

}
#endif


#if UNITY_IPHONE
public class OtherShareUtilsIOS : BaseInviteFrineds
{
	Diffusion m_Diffusion;

	public OtherShareUtilsIOS (Diffusion diffusion)
	{
		m_Diffusion = diffusion;
	}


	protected override void InviteFriends (string message)
	{
		m_Diffusion.Share (message, UIStateManager.Manager.GameDownloadLink, "");
       
	}
}
#endif

