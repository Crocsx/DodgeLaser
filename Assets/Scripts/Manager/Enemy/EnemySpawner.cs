using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy;
    public Transform TOP_LIMIT;
    public Transform BOT_LIMIT;
    public float OFFSET_BORDER = 0.5f;
    public float START_WAVE_TIMER = 2.0f;

    DifficultyLevel currentDifficulty;
    float WaveTimer = 1.5f;
    bool isActive = false;

    void Awake()
    {
        GameManager.instance.OnStartGame += StartGame;
        GameManager.instance.OnFinishGame += FinishGame;
    }

    void StartGame() {
        isActive = true;
    }

    void FinishGame()
    {
        isActive = false;
    }

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
            WaveTimer = DifficultyManager.instance.difficultySelectedSettings.nextWaveTimer;
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
        Vector3 offsetTOP = new Vector3(TOP_LIMIT.position.x, (TOP_LIMIT.position.y - OFFSET_BORDER), TOP_LIMIT.position.z);
        Vector3 offsetBOT = new Vector3(BOT_LIMIT.position.x, (BOT_LIMIT.position.y + OFFSET_BORDER), BOT_LIMIT.position.z);

        Vector3 range = offsetTOP - offsetBOT;
        Vector3 target_position = offsetBOT + Random.value * range ;

        return target_position;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnStartGame -= StartGame;
        GameManager.instance.OnFinishGame -= FinishGame;
    }

    public void OnEnemyDeath(Enemy dead)
    {
        ScoreManager.instance.AddScore(ScoreManager.SCORE_ENEMY_DEAD);
        Destroy(dead.gameObject);
    }
}
