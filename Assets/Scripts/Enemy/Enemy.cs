using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    delegate void eMovement();
    eMovement Movement;
    EnemySpawner spawner;
    BoxCollider2D LaserCollider;

    //Timers
    float EnterTimer = 0.5f;
    float FireWarmUpTimer = 0.1f;
    float AliveTimer = 2f;
    float ExitTimer = 0.5f;

    //Components
    public Transform LaserLeft;
    public Transform LaserRight;

    //Laser Params
    public LineRenderer LaserLine;
    public ParticleSystem particleController;
    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;
    public ParticleSystem particleCenter;

    float LASER_MAX_WIDTH = 0.5f;
    float LASER_MIN_WIDTH = 0.05f;

    //states 
    bool isAlive = false;

    // Use this for initialization
    void Start () {
        Movement = IdleMovement;


        LaserCollider = GetComponent<BoxCollider2D>();
        LaserCollider.enabled = false;

        LaserLine.enabled = false;
        particleController.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        if (!isAlive)
            return;

        LaserMovement();
        Life();
    }

    void LaserMovement()
    {
        LaserLine.SetPosition(0, LaserLeft.GetChild(0).transform.position);
        LaserLine.SetPosition(1, LaserRight.GetChild(0).transform.position);

    }

    void Life()
    {
        AliveTimer -= TimeManager.instance.deltaTime;
        if(AliveTimer < 0)
        {
            StartCoroutine(Exit());
        }
    }

    public virtual void Spawn(EnemySpawner eSpawner)
    {
        spawner = eSpawner;

        LaserLeft.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;
        LaserRight.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;
        LaserLine.startColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g , GetComponent<EnemyType>().color.b, 1f);
        LaserLine.endColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 1f);

        ParticleSystem.MainModule mainL = particleLeft.main;
        mainL.startColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 1f);

        ParticleSystem.MainModule mainR = particleRight.main;
        mainR.startColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 1f);

        ParticleSystem.MainModule mainC = particleCenter.main;
        mainC.startColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 1f);

        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        float timer = 0;

        Vector3 startPosRight = LaserRight.transform.position;
        float sizeRight = LaserRight.GetComponent<SpriteRenderer>().size.x / 2;

        Vector3 startPosLeft = LaserLeft.transform.position;
        float sizeLeft = LaserLeft.GetComponent<SpriteRenderer>().size.x / 2;

        while (timer < EnterTimer)
        {
            LaserLeft.transform.position = Vector3.Lerp(startPosLeft, startPosLeft + new Vector3(sizeLeft, 0, 0), timer / EnterTimer);
            LaserRight.transform.position = Vector3.Lerp(startPosRight, startPosRight + new Vector3(-sizeRight, 0, 0), timer / EnterTimer);

            timer += TimeManager.instance.deltaTime;
            yield return null;
        }

        LaserLeft.transform.position = startPosLeft + new Vector3(sizeLeft, 0, 0);
        LaserRight.transform.position = startPosRight + new Vector3(-sizeRight, 0, 0);

        StartCoroutine(WarmUp());
    }

    IEnumerator WarmUp()
    {
        float timer = 0;
        LaserLine.SetPosition(0, LaserLeft.GetChild(0).transform.position);
        LaserLine.SetPosition(1, LaserRight.GetChild(0).transform.position);

        while (timer < FireWarmUpTimer)
        {
            LaserLine.startColor = new Color(LaserLine.startColor.r, LaserLine.startColor.g, LaserLine.startColor.b, timer / FireWarmUpTimer);
            LaserLine.endColor = new Color(LaserLine.endColor.r, LaserLine.endColor.g, LaserLine.endColor.b, timer / FireWarmUpTimer);
            LaserLine.startWidth = LaserLine.startWidth = Mathf.Lerp(LASER_MAX_WIDTH, LASER_MIN_WIDTH, timer / FireWarmUpTimer);
            timer += TimeManager.instance.deltaTime;
            yield return null;
        }

        LaserLine.startColor = new Color(LaserLine.startColor.r, LaserLine.startColor.g, LaserLine.startColor.b, 1);
        LaserLine.endColor = new Color(LaserLine.endColor.r, LaserLine.endColor.g, LaserLine.endColor.b, 1);
        LaserLine.startWidth = LaserLine.startWidth = LASER_MIN_WIDTH;

        isAlive = true;
        LaserCollider.enabled = true;
        particleController.Play();
        Movement = GetComponent<EnemyType>().Movement;
    }

    IEnumerator Exit()
    {

        float timer = 0;

        isAlive = false;
        LaserLine.startColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 0.0f);
        LaserLine.endColor = new Color(GetComponent<EnemyType>().color.r, GetComponent<EnemyType>().color.g, GetComponent<EnemyType>().color.b, 0.0f);
        Movement = IdleMovement;

        Vector3 startPosRight = LaserRight.transform.position;
        float sizeRight = LaserRight.GetComponent<SpriteRenderer>().size.x / 2;

        Vector3 startPosLeft = LaserLeft.transform.position;
        float sizeLeft = LaserLeft.GetComponent<SpriteRenderer>().size.x / 2;

        while (timer < ExitTimer)
        {
            LaserLeft.transform.position = Vector3.Lerp(startPosLeft, startPosLeft - new Vector3(sizeLeft, 0, 0), timer / ExitTimer);
            LaserRight.transform.position = Vector3.Lerp(startPosRight, startPosRight - new Vector3(-sizeRight, 0, 0), timer / ExitTimer);

            timer += TimeManager.instance.deltaTime;
            yield return null;
        }

        LaserLeft.transform.position = startPosLeft - new Vector3(sizeLeft, 0, 0);
        LaserRight.transform.position = startPosRight - new Vector3(-sizeRight, 0, 0);

        spawner.OnEnemyDeath(this);
    }

    void IdleMovement()
    {

    }
}
