using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private GameObject weapon;
    private float firingRate = 1f;
    private float currentCooldownTimer; // this is Increase equal to attackSpeed
    private bool hasCooldownTimerStarted = false;


    // Use this for initialization
    private void Start()
    {
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
                GamePoolManager.Instance.ReuseObject(bullet, weapon.transform.position, Quaternion.identity);
            }
   }
}
