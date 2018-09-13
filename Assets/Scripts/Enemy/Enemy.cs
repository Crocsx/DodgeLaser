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

    //LIMITS
    public Transform LimitTop;
    public Transform LimitBot;

    //Laser Params
    public LineRenderer LaserLine;
    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;
    public ParticleSystem particleCenter;

    float LASER_MIN_WIDTH = 0.1f;
    float LASER_MAX_WIDTH = 0.5f;

    //states 
    bool isAlive = false;

    // Use this for initialization
    void Start () {
        Movement = IdleMovement;

        LaserCollider = GetComponent<BoxCollider2D>();
        LaserCollider.enabled = false;
        LaserLine.enabled = false;
        StopParticle();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckLimit();
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

        particleCenter.transform.rotation = Quaternion.FromToRotation(Vector3.left, LaserLeft.position - LaserRight.position);
    }

    void Life()
    {
        AliveTimer -= TimeManager.instance.deltaTime;
        if(AliveTimer < 0)
        {
            StartCoroutine(Exit());
        }
    }

    void CheckLimit()
    {
        if (transform.position.y > LimitTop.position.y || transform.position.y < LimitBot.position.y)
            AliveTimer = -1;
    }
    void StopParticle()
    {
        particleCenter.Stop();
        particleCenter.Clear();
        particleLeft.Stop();
        particleRight.Stop();

    }

    void StartParticle()
    {
        particleCenter.Play();
        particleLeft.Play();
        particleRight.Play();
    }

    public virtual void Spawn(EnemySpawner eSpawner)
    {
        spawner = eSpawner;

        LaserLine.material.color = GetComponent<EnemyType>().color;


        LaserLine.material.SetColor("_Color", GetComponent<EnemyType>().color);
        LaserLine.startColor = GetComponent<EnemyType>().color;
        LaserLine.endColor = GetComponent<EnemyType>().color;

        LaserRight.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;
        LaserLeft.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;

        ParticleSystem.MainModule mainL = particleLeft.main;
        mainL.startColor = GetComponent<EnemyType>().color;
        ParticleSystem.MainModule mainR = particleRight.main;
        mainR.startColor = GetComponent<EnemyType>().color;
        ParticleSystem.MainModule mainC = particleCenter.main;
        mainC.startColor = GetComponent<EnemyType>().color;

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
            LaserLine.startWidth = LaserLine.startWidth = Mathf.Lerp(LASER_MIN_WIDTH, LASER_MAX_WIDTH, timer / FireWarmUpTimer);
            LaserLine.endWidth = LaserLine.startWidth = Mathf.Lerp(LASER_MIN_WIDTH, LASER_MAX_WIDTH, timer / FireWarmUpTimer);
            timer += TimeManager.instance.deltaTime;
            yield return null;
        }

        LaserLine.startWidth = LaserLine.endWidth = LASER_MAX_WIDTH;

        Movement = GetComponent<EnemyType>().Movement;
        isAlive = true;
        LaserCollider.enabled = true;
        LaserLine.enabled = true;
        StartParticle();
    }

    IEnumerator Exit()
    {
        StopParticle();
        LaserCollider.enabled = false;
        LaserLine.enabled = false;

        float timer = 0;

        isAlive = false;
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
