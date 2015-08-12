using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour 
{

    public InputField LoginTextbox;
    public Text MessageBox;
    // Use this for initialization
	void Start () 
    {
        LoginTextbox.text = UIStateManager.Manager.PlayerId;
	}
	

    public void DoLogin()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        string loginText = LoginTextbox.text.Trim();
        UIStateManager.Manager.SetLoading(true);
        StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(loginText, OnAuthentication, loginText));
    }

    void OnAuthentication (string errorString,bool result,params object[] userParams)
    {
        Debug.Log(errorString);
        Debug.Log(result);
        if (string.IsNullOrEmpty(errorString) && result)
        {
            UIStateManager.Manager.PlayerId = userParams[0].ToString();
            UIStateManager.Manager.SwapToTournamentPage();
        }
        else
        {
            this.MessageBox.text = "Failed to login";
        }

        UIStateManager.Manager.SetLoading(false);
    }

    public void DoRegister()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.GameAPI.OpenRegistrationUrl();
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToMainMenu();
    }
}
