using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour, EnemyType
{
    public Color color { get; set; }

    void Awake()
    {
        color = Color.red;
    }

    // Update is called once per frame
    void Update () {
	}

    public void Movement()
    {

    }
}
