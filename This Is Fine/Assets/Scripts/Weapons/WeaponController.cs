using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour {

    [SerializeField] protected float firingrate = 1.0f;
    [SerializeField] protected float radius = 1.0f;
    [SerializeField] protected float damage = 1.0f;
    [SerializeField] protected int quality = 1;

    protected SpriteRenderer weaponSpriteRenderer;
}
