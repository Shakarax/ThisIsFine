using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : PoolObject {

    [SerializeField] private float movementSpeed = 10f;

    private Vector3 worldPosition;

    private void Start()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        // this logic is good for pistol
        Vector3 relativePoint = transform.InverseTransformPoint(worldPosition);
        transform.Translate(relativePoint * Time.deltaTime * movementSpeed);
    }

    public override void OnObjectReuse()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



}
