using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyType
{

    Color color { get; set; }

    void Movement();
}
