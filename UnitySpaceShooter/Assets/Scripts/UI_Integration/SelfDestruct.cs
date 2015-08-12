using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
    public float Timer = 7;
	void Start () {
        Invoke("SelfDestroy", Timer);
	}

    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
	
}
