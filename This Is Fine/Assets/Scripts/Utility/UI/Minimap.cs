using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    private Transform target;
    public Transform Target { get { return target; } set { target = value; } }

    private void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = target.position;
        transform.position = newPosition;
    }
}
