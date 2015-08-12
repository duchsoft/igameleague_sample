using UnityEngine;
using System.Collections;

public class SplashGameTitleScript : MonoBehaviour 
{
    public int WaitingTime = 3;
	// Use this for initialization
	void Start () 
    {
        Invoke("WaitAndLoad", WaitingTime);
	}
	
    void WaitAndLoad()
    {
        UIStateManager.Manager.SwapToPoweredLogoSplash();
    }
}
