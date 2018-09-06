using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public bool godmod = true;
    public GameObject ExplosionEffect;

	void Start () {
		
	}

    public void Kill()
    {
        if (godmod)
            return;
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        StageManager.instance.FinishStage();
    }
}
