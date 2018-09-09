using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayed : MonoBehaviour {
    public Text score;

	// Use this for initialization
	void Start () {
        score.text = ScoreManager.instance.highScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
