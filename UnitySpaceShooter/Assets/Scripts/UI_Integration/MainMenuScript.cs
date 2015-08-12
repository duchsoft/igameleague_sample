using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	public Sprite[] soundSprites;
	public Image[] soundButtons;
    public Button CashTournamentsButton;
    public Button PracticeButton;

	// Use this for initialization
	void Start ()
	{
		UpdateSoundButtons ();
	}
    
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void SelectTournaments ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToLogin (false);
	}

	public void SelectHelp ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToHelpPage ();
	}

	public void SelectMoreGames ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.OpenMoreGames ();
	}

	public void SelectPracticeMode ()
	{
		MenuSoundManager.Instance.PlayMenuClick ();
		UIStateManager.Manager.SwapToGame (null, false, string.Empty);
	}

	void UpdateSoundButtons ()
	{
        Sprite s = PlayerPrefs.GetInt("SoundEnabled") == 1 ? soundSprites[0] : soundSprites[1];

		foreach (Image item in soundButtons)
			item.sprite = s;

	}

	public void ChangeSoundState ()
	{
		MenuSoundManager.Instance.ChangeSoundState ();
		UpdateSoundButtons ();
	}
}