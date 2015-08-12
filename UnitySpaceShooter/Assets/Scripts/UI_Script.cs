using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public GameObject LeaderboardButton;
    public GameObject BackButton;
    public RawImage BannerArea;
    public GameObject TapToPlay;
	public GameObject HighScore;
    public static double Score = 0;

    public static bool IsPlayMode = false;
    public static bool GameWin;
    public static bool GameLose;
    public static bool StartGame;

    // Use this for initialization
    void Start()
    {
        StartGame = false;
        GameWin = false;
        GameLose = false;
        TapToPlay.SetActive(true);
        LeaderboardButton.SetActive(false);
        if (!UIStateManager.Manager.CurrentGamePlayMode)
        {
            BackButton.SetActive(true);
        }

        if (UIStateManager.Manager.CurrentGamePlayMode)
        {
			 
            LeaderboardButton.SetActive(true);
			LeaderboardListScript.SetBanner(BannerArea,UIStateManager.Manager.CurrentLeaderboardDetail);
        }

       
    }

    public void SelectLeaderboard()
    {
        UIStateManager.Manager.LoadMenuScene(false);
        UIStateManager.Manager.SwapToLeaderboardListPage(UIStateManager.Manager.CurrentLeaderboardDetail, -1, UIStateManager.Manager.CurrentActivationKey);
    }

    public void SelectBack()
    {
        UIStateManager.Manager.LoadMenuScene(true);
    }

    public void SelectTouchTrigger()
    {
        LeaderboardButton.SetActive(false);
        TapToPlay.SetActive(false);
		BannerArea.gameObject.SetActive(false);
        StartGame = true;
		HighScore.SetActive(true);
    }

    void CalculateScore()
    {
        if (IsPlayMode)
        {           
			HighScore.SetActive(true);
        }
    }

    void Update()
    {
		CalculateScore ();
        if (!UIStateManager.Manager.CurrentGamePlayMode)
        {
            BackButton.SetActive(true);
        }
        else
        {
            BackButton.SetActive(false);
            if (IsPlayMode)
            {
				Debug.Log("working");
                BannerArea.gameObject.SetActive(false);
				Destroy(BannerArea.gameObject);
                LeaderboardButton.SetActive(false);
                CalculateScore();
				HighScore.SetActive(true);
            }
        }
    }
}