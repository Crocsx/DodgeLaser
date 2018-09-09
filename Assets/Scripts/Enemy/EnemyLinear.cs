using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLinear: MonoBehaviour, EnemyType
{
    public Color color { get; set; }
    public float linearSpeed = 0.5f;
    public int side;

    void Awake()
    {
        color = Color.green;
        side = Random.Range(0, 2) * 2 - 1;
    }

    public void Movement()
    {
        transform.position += new Vector3(0, side * linearSpeed, 0) * TimeManager.instance.deltaTime;
    }
}
