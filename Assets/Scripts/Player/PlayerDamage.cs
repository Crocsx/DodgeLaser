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
        Invoke("End", ExplosionEffect.GetComponent<ParticleSystem>().main.duration);
    }

    public void End()
    {
        StageManager.instance.FinishStage();
    }
}
