using UnityEngine;
using System.Collections;

public class G4S_InGame_UI_Manager : MonoBehaviour {

    bool startGame = false;
    public GameObject TapToPlay_Button;
    public GameObject Leaderboards_Button;
    public GameObject Back_Button;
    public Done_GameController gameController;
	
	void Start () {

        gameController = new Done_GameController();
        TapToPlay_Button.SetActive(true);
        if (UIStateManager.Manager.CurrentGamePlayMode)
        {
            Leaderboards_Button.SetActive(true);
        }
        else
        {
            Back_Button.SetActive(true);
        }
	}

    public void DoStartGame()
    {
        startGame = true;
        gameController.StartCoroutine(gameController.SpawnWaves());
        TapToPlay_Button.SetActive(false);
        Leaderboards_Button.SetActive(false);
        Back_Button.SetActive(false);
    }

    public void SelectLeaderboards()
    {

    }

    public void SelectBack()
    {

    }
}
