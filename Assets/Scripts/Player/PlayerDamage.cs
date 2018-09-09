using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public GameObject ExplosionEffect;

	void Start () {
		
	}

    public void Kill()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        StageManager.instance.FinishStage();
    }
}
