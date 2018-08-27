using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : PoolObject {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int minSpreadRange = -10;
    [SerializeField] private int maxSpreadRange = 10;

    float spreadAngle;
    float rotateAngle;
    Vector2 movementDirection;

    private Vector3 worldPosition;

    private void Start()
    {
        spreadAngle = Random.RandomRange(minSpreadRange, maxSpreadRange);
        var x = PlayerController.Instance.GetComponentInChildren<ShotgunBulletManager>().BulletSpawnLoc1.position.x - PlayerController.Instance.MyTransform.position.x;
        var y = PlayerController.Instance.GetComponentInChildren<ShotgunBulletManager>().BulletSpawnLoc1.position.y - PlayerController.Instance.MyTransform.position.y;
        rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
        movementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad));
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(movementDirection * Time.deltaTime * movementSpeed);
    }

    public override void OnObjectReuse()
    {
        spreadAngle = Random.RandomRange(minSpreadRange, maxSpreadRange);
        var x = PlayerController.Instance.GetComponentInChildren<ShotgunBulletManager>().BulletSpawnLoc1.position.x - PlayerController.Instance.MyTransform.position.x;
        var y = PlayerController.Instance.GetComponentInChildren<ShotgunBulletManager>().BulletSpawnLoc1.position.y - PlayerController.Instance.MyTransform.position.y;
        rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
        movementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

}
