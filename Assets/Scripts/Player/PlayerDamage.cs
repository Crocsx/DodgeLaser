using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

	void Start () {
		
	}

    public void Kill()
    {
        StageManager.instance.FinishStage();
    }
}
