using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : PoolObject {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    private void FixedUpdate()
    {
        // this logic is good for pistol
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePoint = transform.InverseTransformPoint(worldPosition);
        transform.Translate(relativePoint * Time.deltaTime * movementSpeed);
    }
}
