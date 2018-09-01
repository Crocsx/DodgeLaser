using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnEndAnim () {
        GameManager.instance.LoadScene("MainMenu");
	}
}
