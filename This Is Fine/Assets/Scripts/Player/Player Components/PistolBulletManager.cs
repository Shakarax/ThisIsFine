using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletManager : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxPoolSize = 20;
    [SerializeField] private Transform bulletSpawnLoc;
    private float firingRate;
    private float currentCooldownTimer; // this is Increase equal to attackSpeed
    private bool hasCooldownTimerStarted = false;
	public AudioClip shoot;


    // Use this for initialization
    private void Start()
    {
        firingRate = PlayerController.Instance.GetComponentInChildren<PistolController>().FiringRate;
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
                GamePoolManager.Instance.ReuseObject(bullet, bulletSpawnLoc.position, Quaternion.identity);
				AudioSource shoot = GetComponent<AudioSource>();
				shoot.Play ();
            }
   }
}
