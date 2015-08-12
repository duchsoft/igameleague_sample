using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToMainMenu();
    }
}
