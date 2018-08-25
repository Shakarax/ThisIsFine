using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private int fireScoreValue = 100;

    private bool doScaleFireOne = true;
    private bool doScaleFireTwo = true;
    private bool doScaleFireThree = true;

    // Update is called once per frame
    void Update () {
        IsFireDead();
        FireScaler();
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
    }

    private void IsFireDead()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // 100% hp == 100% local scale
    // 66% hp == 75% local scale
    // 33% hp == 50% local scale
    private void FireScaler()
    {

        float percentageHp = currentHp / 100;

        if (percentageHp > .66) {
            if (doScaleFireOne){
                transform.localScale = new Vector3(transform.localScale.x * 1f, transform.localScale.y * 1f, transform.localScale.z * 1f);
                doScaleFireOne = false;
            }
        } else if (percentageHp <= .66 && percentageHp > .33 ) {
            if (doScaleFireTwo && !doScaleFireOne){
                transform.localScale = new Vector3(transform.localScale.x * .76f, transform.localScale.y * .76f, transform.localScale.z * .76f);
                doScaleFireTwo = false;
            }
        } else {
            if (doScaleFireThree && !doScaleFireTwo){
                transform.localScale = new Vector3(transform.localScale.x * .70f, transform.localScale.y * .70f, transform.localScale.z * .70f);
                doScaleFireThree = false;
            }
        }
    }
}
