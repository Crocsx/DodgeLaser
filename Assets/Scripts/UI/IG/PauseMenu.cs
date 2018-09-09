using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        GetComponent<Animator>().SetBool("entering",true);
    }

    public void Entered()
    {
        GetComponent<Animator>().SetBool("entering", false);
        GetComponent<Animator>().SetBool("isEntered", true);
    }

    public void Exiting()
    {
        GetComponent<Animator>().SetBool("entering", false);
        GetComponent<Animator>().SetBool("isEntered", false);
        GetComponent<Animator>().SetBool("exiting", false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
