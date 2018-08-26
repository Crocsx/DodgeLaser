using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTouch(TouchStruct touchStruct)
    {
    }

    public void StartGame()
    {
        GameManager.instance.LoadScene("Stage01");
    }
}
