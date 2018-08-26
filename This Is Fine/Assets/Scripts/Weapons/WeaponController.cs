using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour {

    [SerializeField] protected float firingrate = 1.0f;
    [SerializeField] protected float damage = 1.0f;
    [SerializeField] protected int quality = 1;

    public int Quality{
        get { return quality; }
        set { quality = value; }
     }

    public float FiringRate
    {
        get {return firingrate;}
        set { firingrate = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    protected SpriteRenderer weaponSpriteRenderer;
}
