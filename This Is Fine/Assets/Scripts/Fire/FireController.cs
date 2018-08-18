using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        IsFireDead();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            Debug.Log("I HIT YOU FIRE!");
            currentHp -= PlayerController.Instance.GetComponentInChildren<WeaponController>().Damage;
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
}
