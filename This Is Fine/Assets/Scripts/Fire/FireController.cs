using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : PoolObject {

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private int fireScoreValue = 100;
    [SerializeField] private float fireSpreadCooldown = 100f;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private int maxPoolSize;

    private float currentCooldownTimer;
    private int spawnCount = 0;
    private bool hasFireSpreadCooldownTimerStarted = false;
    private bool doScaleFireOne = true;
    private bool doScaleFireTwo = true;
    private bool doScaleFireThree = true;

    private enum FireSpawnDirection { Left, Right, Up, Down };

    private void Start()
    {
        // Creates fire spread pool
        GamePoolManager.Instance.CreatePool(firePrefab, maxPoolSize);
    }

    // Update is called once per frame
    private void Update() {
        IsFireDead();
        FireScaler();
        spawnAdditionalFire();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PistolBullet")
        {
            currentHp -= PlayerController.Instance.GetComponentInChildren<PistolController>().Damage;
            PlayerController.Instance.PlayerScoreCount += fireScoreValue;
            collider.gameObject.SetActive(false);
        }
        if (collider.tag == "ShotgunBullet")
        {
            currentHp -= PlayerController.Instance.GetComponentInChildren<ShotgunController>().Damage;
            PlayerController.Instance.PlayerScoreCount += fireScoreValue;
            collider.gameObject.SetActive(false);
        }
        if (collider.tag == "Fire" || collider.tag == "Wall")
        {
            FireManager.Instance.FireCount--;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Fire" || collider.tag == "Wall")
        {
            FireManager.Instance.FireCount--;
            gameObject.SetActive(false);
        }
    }

    private void IsFireDead()
    {
        if (currentHp <= 0)
        {
            FireManager.Instance.FireCount--;
            gameObject.SetActive(false);
        }
    }

    // 100% hp == 100% local scale
    // 66% hp == 75% local scale
    // 33% hp == 50% local scale
    private void FireScaler()
    {

        float percentageHp = currentHp / 100;

        if (percentageHp > .66) {
            if (doScaleFireOne) {
                transform.localScale = new Vector3(transform.localScale.x * 1f, transform.localScale.y * 1f, transform.localScale.z * 1f);
                doScaleFireOne = false;
            }
        } else if (percentageHp <= .66 && percentageHp > .33) {
            if (doScaleFireTwo && !doScaleFireOne) {
                transform.localScale = new Vector3(transform.localScale.x * .76f, transform.localScale.y * .76f, transform.localScale.z * .76f);
                doScaleFireTwo = false;
            }
        } else {
            if (doScaleFireThree && !doScaleFireTwo) {
                transform.localScale = new Vector3(transform.localScale.x * .70f, transform.localScale.y * .70f, transform.localScale.z * .70f);
                doScaleFireThree = false;
            }
        }
    }

    public override void OnObjectReuse()
    {
        currentHp = maxHp;
        transform.localScale = new Vector3(.6f, .6f, .6f);
    }

    private void spawnAdditionalFire()
    {
        if (hasFireSpreadCooldownTimerStarted)
        {
            spawnCount++;
            currentCooldownTimer -= Time.deltaTime;
            if (currentCooldownTimer <= 0)
            {
                hasFireSpreadCooldownTimerStarted = false;
                currentCooldownTimer = fireSpreadCooldown;
            }
        }
        if (!hasFireSpreadCooldownTimerStarted)
        {
            hasFireSpreadCooldownTimerStarted = true;
            if (spawnCount > 1)
            {
                Vector2 spawnLoc;
                int randomDirection = Random.Range(0, 4);
                if (randomDirection == (int)FireSpawnDirection.Left) { spawnLoc = new Vector2(transform.position.x - 2, transform.position.y); }
                else if (randomDirection == (int)FireSpawnDirection.Right) { spawnLoc = new Vector2(transform.position.x + 2, transform.position.y); }
                else if (randomDirection == (int)FireSpawnDirection.Up) { spawnLoc = new Vector2(transform.position.x, transform.position.y + 2); }
                else { spawnLoc = new Vector2(transform.position.x, transform.position.y - 2); }
                GamePoolManager.Instance.ReuseObject(firePrefab, spawnLoc, Quaternion.identity);
                FireManager.Instance.FireCount++;
            }
        }
    }

}
