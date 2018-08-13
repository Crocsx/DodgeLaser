using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    delegate void eMovement();
    eMovement Movement;

    float SpawnTimer = 0.5f;
    float FireWarmUpTimer = 0.1f;
 
    public Transform LaserLeft;
    public Transform LaserRight;

    // Use this for initialization
    void Start () {
        Movement = IdleMovement;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    public virtual void Spawn()
    {
        LaserLeft.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;
        LaserRight.GetComponent<SpriteRenderer>().color = GetComponent<EnemyType>().color;

        StartCoroutine(SpawnEnter());
    }

    IEnumerator SpawnEnter()
    {
        float timer = 0;

        Vector3 startPosRight = LaserRight.transform.position;
        float sizeRight = LaserRight.GetComponent<SpriteRenderer>().size.x / 2;

        Vector3 startPosLeft = LaserLeft.transform.position;
        float sizeLeft = LaserLeft.GetComponent<SpriteRenderer>().size.x / 2;

        while (timer < SpawnTimer)
        {
            LaserLeft.transform.position = Vector3.Lerp(startPosLeft, startPosLeft + new Vector3(sizeLeft, 0, 0), timer / SpawnTimer);
            LaserRight.transform.position = Vector3.Lerp(startPosRight, startPosRight + new Vector3(-sizeRight, 0, 0), timer / SpawnTimer);
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

        while (timer < FireWarmUpTimer)
        {
            timer += TimeManager.instance.deltaTime;
            yield return null;
        }
        Movement = GetComponent<EnemyType>().Movement;
    }

    void IdleMovement()
    {

    }
}
