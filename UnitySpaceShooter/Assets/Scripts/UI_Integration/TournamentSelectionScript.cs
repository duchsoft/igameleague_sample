using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TournamentSelectionScript : MonoBehaviour
{
    public Text UserLoginMessage;
    public Button SwitchUser;
    public Button UserTournament;
    public Button GlobalTournament;
    public Button MyTournaments;

    void OnAuthentication(string errorString, bool result, params object[] userParams)
    {
        LeaderboardStorage[] LeaderboardStorageCache = UIStateManager.Manager.SettingsSystem.GetLeaderboardCache(UIStateManager.Manager.PlayerId);
        LeaderboardStorage[] FavStorageCache = UIStateManager.Manager.SettingsSystem.GetFavs(UIStateManager.Manager.PlayerId);
        if (LeaderboardStorageCache.Length > 0 || FavStorageCache.Length > 0)
        {
            MyTournaments.interactable = true;
        }
        else
        {
            MyTournaments.interactable = false;
        }
        if (!string.IsNullOrEmpty(errorString))
        {
            SwitchUser.interactable = false;
            UserTournament.interactable = false;
            GlobalTournament.interactable = false;
        }
    }

    void Start ()
	{
        UserLoginMessage.text = string.Format("Signed in as {0}", UIStateManager.Manager.PlayerId);

        if (!String.IsNullOrEmpty(UIStateManager.Manager.PlayerId))
        {
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(UIStateManager.Manager.PlayerId, OnAuthentication));
        }

        UIStateManager.Manager.OnConnectionChange += Manager_OnConnectionChange;
	}

    void OnDestroy()
    {
        UIStateManager.Manager.OnConnectionChange -= Manager_OnConnectionChange;
    }

    void Manager_OnConnectionChange(bool online)
    {
        SwitchUser.interactable = online;
        UserTournament.interactable = online;
        GlobalTournament.interactable = online;
    }

	public void SelectGlobalTournament ()
	{
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToLeaderboardPage();
	}

	public void SelectUserTournament ()
	{
        MenuSoundManager.Instance.PlayMenuClick();
		UIStateManager.Manager.SwapToUserTournamentsPage ();
	}

	public void SelectBack ()
	{
        MenuSoundManager.Instance.PlayMenuClick();
		UIStateManager.Manager.SwapToMainMenu ();
	}

    public void SelectPreviouslyPlayed()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToSavedTournamentsPage();
    }

    public void SelectSwitchLogin()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToLogin(true);
    }
}