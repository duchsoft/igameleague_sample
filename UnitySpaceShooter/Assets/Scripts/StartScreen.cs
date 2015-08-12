using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartScreen : MonoBehaviour {

		public GameObject StartScreenObject;
		public static  int ShowHelpOnlyOnce;
		public GameObject HighScore_Text;
		string BestScore;
		bool m_Online = true;
		// Use this for initialization
		void Start () {
		



		Debug.Log (BestScore);
						ShowHelpOnlyOnce = PlayerPrefs.GetInt ("ShowHelpOnlyOnce");
		if (ShowHelpOnlyOnce == 1)
			StartScreenObject.SetActive (false);
		else
			StartScreenObject.SetActive (true);




		if (UIStateManager.Manager.CurrentGamePlayMode)
		{
			StartCoroutine(
				UIStateManager.GameAPI.GetPlayerRankDetail(
				UIStateManager.Manager.PlayerId,
				UIStateManager.Manager.CurrentLeaderboardDetail.Id,
				OnGetRankDetails));
		}
		
		UIStateManager.Manager.OnConnectionChange += Manager_OnConnectionChange;
		}
		
		// Update is called once per frame
		void Update () {
		if (UIStateManager.Manager.CurrentGamePlayMode) {
			HighScore_Text.GetComponent<Text> ().text = BestScore;
			
		}
		else
		{
			HighScore_Text.GetComponent<Text> ().text = ((int)PlayerPrefs.GetFloat (Application.loadedLevelName + "bestScore")).ToString(); 
			
		}



		if (ShowHelpOnlyOnce == 1)
			StartScreenObject.SetActive (false);
		else
			StartScreenObject.SetActive (true);
			PlayerPrefs.SetInt ("ShowHelpOnlyOnce", ShowHelpOnlyOnce);
			//Saving the variables
			PlayerPrefs.Save ();
			
			//ShowLastHighScore.text = ( HighScore.ToString());
		}

	public 	void SetPlay()
	{

		ShowHelpOnlyOnce = 1;
	}

  
	
	void Manager_OnConnectionChange(bool online)
	{
		if (m_Online != online)
		{
			if (online)
			{
				//Online Score
				StartCoroutine(
					UIStateManager.GameAPI.GetPlayerRankDetail(
					UIStateManager.Manager.PlayerId,
					UIStateManager.Manager.CurrentLeaderboardDetail.Id,
					OnGetRankDetails));
			}
			else
			{
				
				//Offline Score
				GetHighScoreOffline(UIStateManager.Manager.CurrentLeaderboardDetail);
			}
		}
		
		m_Online = online;
		
	}
	
	void OnGetRankDetails(string errorString, RankDetail result, params object[] userParam)
	{
		if (string.IsNullOrEmpty(errorString))
		{
			BestScore = "0";
			
			if (result != null)
			{
				BestScore = result.Score.ToString();
			}
		}
		else
		{
			GetHighScoreOffline(UIStateManager.Manager.CurrentLeaderboardDetail);
		}
	}
	
	void GetHighScoreOffline(LeaderboardDetail detail)
	{
		BestScore = UIStateManager.Manager.ScoreHistorySystem.GetHighScore(UIStateManager.Manager.PlayerId, detail).ToString();
	}
}

