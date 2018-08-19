using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletManager : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxPoolSize = 30;
    [SerializeField] private Transform bulletSpawnLoc1;
    [SerializeField] private Transform bulletSpawnLoc2;
    [SerializeField] private Transform bulletSpawnLoc3;
    private float firingRate;
    private float currentCooldownTimer; // this is Increase equal to attackSpeed
    private bool hasCooldownTimerStarted = false;

    public Transform BulletSpawnLoc1 { get { return bulletSpawnLoc1; } }

    // Use this for initialization
    private void Start()
    {
        firingRate = PlayerController.Instance.GetComponentInChildren<ShotgunController>().FiringRate;
        GamePoolManager.Instance.CreatePool(bullet, maxPoolSize);
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasCooldownTimerStarted)
        {
            currentCooldownTimer -= Time.deltaTime;
            if (currentCooldownTimer <= 0)
            {
                hasCooldownTimerStarted = false;
                currentCooldownTimer = firingRate;
            }
        }
        if (!hasCooldownTimerStarted)
        {
            hasCooldownTimerStarted = true;
            GamePoolManager.Instance.ReuseObject(bullet, bulletSpawnLoc1.transform.position, Quaternion.identity);
            GamePoolManager.Instance.ReuseObject(bullet, bulletSpawnLoc2.transform.position, Quaternion.identity);
            GamePoolManager.Instance.ReuseObject(bullet, bulletSpawnLoc3.transform.position, Quaternion.identity);
        }
    }
}
