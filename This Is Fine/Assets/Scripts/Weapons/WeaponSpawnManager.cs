using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour {

    [SerializeField] private List<GameObject> qualityOneList;
    [SerializeField] private List<GameObject> qualityTwoList;
    [SerializeField] private List<GameObject> qualityThreeList;
    [SerializeField] private List<GameObject> qualityFourList;
    [SerializeField] private float qualityTimer;
    [SerializeField] private float qualityOneTime = 0f;
    [SerializeField] private float qualityTwoTime = 30f;
    [SerializeField] private float qualityThreeTime = 60f;
    [SerializeField] private float qualityFourTime = 90f;

    private GameObject[] weaponSpawnPoints;
    private int currentQuality;
    private bool hasChangedQualityOne;
    private bool hasChangedQualityTwo;
    private bool hasChangedQualityThree;
    private bool hasChangedQualityFour;

    private static WeaponSpawnManager instance;
    public static WeaponSpawnManager Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<WeaponSpawnManager>(); }
            return instance;
        }
    }

    // Use this for initialization
    private void Start () {
        hasChangedQualityOne = false;
        hasChangedQualityTwo = false;
        hasChangedQualityThree = false;
        hasChangedQualityFour = false;
        qualityTimer = 0;
        currentQuality = 1;
        weaponSpawnPoints = GameObject.FindGameObjectsWithTag("WeaponSpawnPoint");
        for (int i = 0; i < 2; i++)
        {
            GamePoolManager.Instance.CreatePool(qualityOneList[i], 15);
            GamePoolManager.Instance.CreatePool(qualityTwoList[i], 15);
            GamePoolManager.Instance.CreatePool(qualityThreeList[i], 15);
            GamePoolManager.Instance.CreatePool(qualityFourList[i], 15);
        }
    }
	
	// Update is called once per frame
	private void Update () {
        HandleWeaponSpawning();
	}

    private void SpawnWeaponHandler(int currentQuality){

        foreach (GameObject spawn in weaponSpawnPoints)
        {
            Vector2 spawnLoc = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
            int randomWeapon = Random.Range(0, 2);
            GameObject weaponObject;

            if (currentQuality == 1) { weaponObject = (randomWeapon > 0) ? qualityOneList[randomWeapon] : qualityOneList[randomWeapon]; }
            else if (currentQuality == 2) { weaponObject = (randomWeapon > 0) ? qualityTwoList[randomWeapon] : qualityTwoList[randomWeapon]; }
            else if (currentQuality == 3) { weaponObject = (randomWeapon > 0) ? qualityThreeList[randomWeapon] : qualityThreeList[randomWeapon]; }
            else { weaponObject = (randomWeapon > 0) ? qualityFourList[randomWeapon] : qualityFourList[randomWeapon]; }

            GamePoolManager.Instance.ReuseObject(weaponObject, spawnLoc, Quaternion.identity);
        }
    }

    public void DeSpawnWeaponHandler()
    {
        if (GameObject.FindGameObjectsWithTag("Pistol") != null)
        {
            GameObject[] activePistols = GameObject.FindGameObjectsWithTag("Pistol");
            foreach (GameObject pistol in activePistols)
            {
                pistol.SetActive(false);
            }
        }

        if (GameObject.FindGameObjectsWithTag("Shotgun") != null)
        {
            GameObject[] activeShotguns = GameObject.FindGameObjectsWithTag("Shotgun");
            foreach (GameObject shotgun in activeShotguns)
            {
                shotgun.SetActive(false);
            }
        }

    }

    public void HandleWeaponSpawning()
    {
        if (qualityTimer < qualityFourTime) { qualityTimer += Time.deltaTime; }

        if (qualityTimer < qualityTwoTime && !hasChangedQualityOne)
        {
            Debug.Log("Quality Changed to 1");
            currentQuality = 1;
            DeSpawnWeaponHandler();
            SpawnWeaponHandler(currentQuality);
            hasChangedQualityOne = true;
        }
        else if (qualityTimer >= qualityTwoTime && qualityTimer < qualityThreeTime && !hasChangedQualityTwo)
        {
            Debug.Log("Quality Changed to 2");
            currentQuality = 2;
            DeSpawnWeaponHandler();
            SpawnWeaponHandler(currentQuality);
            hasChangedQualityTwo = true;
        }
        else if (qualityTimer >= qualityThreeTime && qualityTimer < qualityFourTime && !hasChangedQualityThree)
        {
            Debug.Log("Quality Changed to 3");
            currentQuality = 3;
            DeSpawnWeaponHandler();
            SpawnWeaponHandler(currentQuality);
            hasChangedQualityThree = true;
        }
        else if (qualityTimer >= qualityFourTime && !hasChangedQualityFour)
        {
            Debug.Log("Quality Changed to 4");
            currentQuality = 4;
            DeSpawnWeaponHandler();
            SpawnWeaponHandler(currentQuality);
            hasChangedQualityFour = true;
        }
    }
}
