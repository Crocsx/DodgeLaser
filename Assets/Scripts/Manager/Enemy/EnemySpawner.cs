using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy;
    public Transform TOP_LIMIT;
    public Transform BOT_LIMIT;
    public float WAVE_TIMER = 2.0f;

    float WaveTimer = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WaveTimer -= TimeManager.instance.deltaTime;
        if(WaveTimer < 0)
        {
            SpawnWave();
            WaveTimer = WAVE_TIMER;
        }
    }

    void SpawnWave()
    {
        Vector3 spawnPoint = GetSpawningPoint();
        GameObject nEnemy = Instantiate(Enemy, spawnPoint, Quaternion.identity) as GameObject;
        nEnemy.AddComponent<EnemyDiagonal>();
        nEnemy.GetComponent<Enemy>().Spawn();
    }

    Vector2 GetSpawningPoint()
    {
        Vector3 v = TOP_LIMIT.position - BOT_LIMIT.position;
        Vector3 target_position = BOT_LIMIT.position + Random.value * v;
        return target_position;
    }
}
