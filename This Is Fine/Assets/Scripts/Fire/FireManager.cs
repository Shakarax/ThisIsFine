using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireManager : MonoBehaviour {

    [SerializeField] private GameObject fireObject;
    [SerializeField] private int maxPoolSize;

    private GameObject[] fireSpawnPoints;
    public int CurrentLevel { get; set; }
    private int fireDamage = 20;
    public int FireDamage
    {
        get { return fireDamage; }
    }

    private static FireManager instance;
    public static FireManager Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<FireManager>(); }
            return instance;
        }
    }

    // Use this for initialization
    private void Start () {
        CurrentLevel = 1;
        GamePoolManager.Instance.CreatePool(fireObject, maxPoolSize);
        SpawnFireHandler();
    }
	
	// Update is called once per frame
	private void Update () {
        
    }

    public void SpawnFireHandler(){
        
        fireSpawnPoints = GameObject.FindGameObjectsWithTag("FireSpawnPoint");
        foreach (GameObject spawn in fireSpawnPoints)
        {
            Vector2 spawnLoc = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
            GamePoolManager.Instance.ReuseObject(fireObject, spawnLoc, Quaternion.identity);
            PlayerController.Instance.FireCount++;
        }
    }

    public void DeSpawnFireHandler()
    {
        if (GameObject.FindGameObjectsWithTag("Fire") != null)
        {
            GameObject[] activeFires = GameObject.FindGameObjectsWithTag("Fire");
            foreach (GameObject fire in activeFires)
            {
                fire.SetActive(false);
            }
        }
        PlayerController.Instance.FireCount = 0;
    }

}
