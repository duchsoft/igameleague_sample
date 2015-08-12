using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour
{
	
	public GameObject Background;
	public float TurnOnAfterSecs = 2;
	static int counter = 0;
	
	void Awake()
	{
		 
		if (counter == 0)
		{
			Background.SetActive(false);
		}
	}
	
	void Start()
	{
		Invoke("TurnMeOn", TurnOnAfterSecs);
	}
	
	void TurnMeOn()
	{
		Background.SetActive(true);
		counter++;
	}
}
