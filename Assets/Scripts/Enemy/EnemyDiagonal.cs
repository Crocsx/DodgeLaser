using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiagonal : MonoBehaviour, EnemyType
{
    public Color color { get; set; }
    public float linearSpeed = 0.01f;
    public int side;

    Enemy parent;
    
    void Awake()
    {
        color = Color.blue;
        side = Random.Range(0, 2) * 2 - 1;
        parent = transform.GetComponent<Enemy>();
    }

    public void Movement()
    {
        parent.LaserLeft.transform.position += new Vector3(0, side * linearSpeed, 0) * TimeManager.instance.deltaTime;
        parent.LaserRight.transform.position += new Vector3(0, -side * linearSpeed, 0) * TimeManager.instance.deltaTime;
    }
}
