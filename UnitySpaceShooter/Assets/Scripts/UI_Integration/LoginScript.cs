using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public InputField LoginTextbox;
    public Text MessageBox;

    public Text SignUpUrl;
    public Text SelectCurrencyText;
    public GameObject CurrencyTypeDropdown;
    bool didSelectCurrency = true; //ToDo: False when there are multiple currencies

    const string inr = "INR";
    const string usd = "USD";
    const string usdUrl = " Sign up \n (www.igameleague.com)";
    const string inrUrl = " Sign up \n (www.igameleague.in)";

    void Start()
    {
        LoginTextbox.text = UIStateManager.Manager.PlayerId;
    }

    public void SelectCurrency(int currencyType)
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.currencyType = (CurrencyType)currencyType;
        didSelectCurrency = true;
        UIStateManager.Manager.SetCurrencyType(currencyType);
        if (UIStateManager.Manager.currencyType == CurrencyType.INR)
        {
            SelectCurrencyText.text = inr;
            SignUpUrl.text = inrUrl;
            CurrencyTypeDropdown.SetActive(false);
        }

        if (UIStateManager.Manager.currencyType == CurrencyType.USD)
        {
            SelectCurrencyText.text = usd;
            SignUpUrl.text = usdUrl;
            CurrencyTypeDropdown.SetActive(false);
        }
    }

    public void SelectCurrencyDropdown()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        CurrencyTypeDropdown.SetActive(true);     //Todo: Uncomment on multiple currency mode
    }

    public void SelectClose()
    {
        CurrencyTypeDropdown.SetActive(false);
    }

    public void DoLogin()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        string loginText = LoginTextbox.text.Trim();
        if (didSelectCurrency)
        {
            UIStateManager.Manager.SetLoading(true);
            StartCoroutine(UIStateManager.GameAPI.IsActivePlayer(loginText, OnAuthentication, loginText));
        }
        else
        {
            MessageBox.text = "Please select a currency to continue...";
        }
    }

    void OnAuthentication(string errorString, bool result, params object[] userParams)
    {
        Debug.Log(errorString);
        Debug.Log(result);
        Debug.Log(IGL.URL_FORMAT);
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
