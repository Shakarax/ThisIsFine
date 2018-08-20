using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : WeaponController {

    [SerializeField] private float qualityOneDamage = 10f;
    [SerializeField] private float qualityTwoDamage = 20f;
    [SerializeField] private float qualityThreeDamage = 30f;
    [SerializeField] private float qualityFourDamage = 40f;

    private int currentQuality;
    private WeaponController playerWeapon;

    private void Start()
    {
        playerWeapon = GetComponent<WeaponController>();
        currentQuality = playerWeapon.Quality;
    }

    private void Update()
    {
        WeaponQualityDamageUpdate();
    }

    private void WeaponQualityDamageUpdate()
    {
        currentQuality = playerWeapon.Quality;
        if (currentQuality == 1)
        {
            playerWeapon.Damage = qualityOneDamage;
        }
        else if (currentQuality == 2)
        {
            playerWeapon.Damage = qualityTwoDamage;
        }
        else if (currentQuality == 3)
        {
            playerWeapon.Damage = qualityThreeDamage;
        }
        else if (currentQuality == 4)
        {
            playerWeapon.Damage = qualityFourDamage;
        }
    }
}
