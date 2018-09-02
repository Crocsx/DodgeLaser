using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    void Update()
    {
        transform.LookAt(target.position + offset);
        transform.Translate(Vector3.right * Time.deltaTime);
    }

}
