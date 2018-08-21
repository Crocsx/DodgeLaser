using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy;
    public Transform TOP_LIMIT;
    public Transform BOT_LIMIT;
    public float WAVE_TIMER = 2.0f;
     
    float WaveTimer = 2.0f;
    bool isActive = false;

    void Awake()
    {
        GameManager.instance.OnStartGame += StartGame;
    }
    // Use this for initialization
    void StartGame() {
        isActive = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
            WaveHandler();
    }

    void WaveHandler()
    {
        WaveTimer -= TimeManager.instance.deltaTime;
        if (WaveTimer < 0)
        {
            SpawnWave();
            WaveTimer = WAVE_TIMER;
        }
    }

    void SpawnWave()
    {
        Vector3 spawnPoint = GetSpawningPoint();
        GameObject nEnemy = Instantiate(Enemy, spawnPoint, Quaternion.identity) as GameObject;

        System.Type type = RandomEnemyScript();
        if (type == typeof(EnemyDiagonal))
        {
            nEnemy.AddComponent<EnemyDiagonal>();
        }
        else if (type == typeof(EnemyLinear))
        {
            nEnemy.AddComponent<EnemyLinear>();
        }
        else if (type == typeof(EnemyStatic))
        {
            nEnemy.AddComponent<EnemyStatic>();
        }
        
        nEnemy.GetComponent<Enemy>().Spawn(this);
    }

    System.Type RandomEnemyScript()
    {
        System.Type[] classTypes = new System.Type[] { typeof(EnemyDiagonal), typeof(EnemyLinear), typeof(EnemyStatic) };
        System.Type type = classTypes[Random.Range(0, 3)];
        return type;
    }

    Vector2 GetSpawningPoint()
    {
        Vector3 v = TOP_LIMIT.position - BOT_LIMIT.position;
        Vector3 target_position = BOT_LIMIT.position + Random.value * v;
        return target_position;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnStartGame -= StartGame;
    }

    public void OnEnemyDeath(Enemy dead)
    {
        ScoreManager.instance.AddScore(ScoreManager.SCORE_ENEMY_DEAD);
        Destroy(dead.gameObject);
    }
}
