using UnityEngine;
using System.Collections;

public class SplashCompanyLogoScript : MonoBehaviour 
{
    public int WaitingTime = 2;
	// Use this for initialization
	void Start () 
    {
        Invoke("WaitAndLoad", WaitingTime);
	}
	
    void WaitAndLoad()
    {
        UIStateManager.Manager.SwapToTitleSplash();
    }
}