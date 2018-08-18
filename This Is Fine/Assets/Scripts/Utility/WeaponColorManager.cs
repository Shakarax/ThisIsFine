using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColorManager : MonoBehaviour {

    SpriteRenderer weaponSpriteRenderer;
    WeaponController weapon;

    private void Start()
    {
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponent<WeaponController>();
        weaponSpriteRenderer.color = ChangingWeaponColor(weapon.Quality); 
    }

    // Change Color of Weapon based on quality
    private Color ChangingWeaponColor(int quality)
    {
        Color greenColor = new Color32(105, 236, 104, 255);
        Color blueColor = new Color32(0, 45, 255, 255);
        Color purpleColor = new Color32(198, 0, 255, 255);
        Color goldColor = new Color32(255, 208, 0, 255);

        if (quality == 1) { return greenColor; }
        else if (quality == 2) { return blueColor; }
        else if (quality == 3) { return purpleColor; }
        else if (quality == 4) { return goldColor; }
        else { return greenColor; }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerController.Instance.WeaponSprite.color = weaponSpriteRenderer.color;
            PlayerController.Instance.GetComponentInChildren<WeaponController>().Quality = weapon.Quality;
            Destroy(gameObject);
        }
    }

}
