using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((ScoreManager.instance.score > 15 && ScoreManager.instance.score < 30) && DifficultyManager.instance.difficultySelected != DifficultyManager.DifficultyAvailable.Medium)
            DifficultyManager.instance.ChangeDifficulty(DifficultyManager.DifficultyAvailable.Medium);

        if (ScoreManager.instance.score > 30 && DifficultyManager.instance.difficultySelected != DifficultyManager.DifficultyAvailable.Hard)
            DifficultyManager.instance.ChangeDifficulty(DifficultyManager.DifficultyAvailable.Hard);
    }
}
