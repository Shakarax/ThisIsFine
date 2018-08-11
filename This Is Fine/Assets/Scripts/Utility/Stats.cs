using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stats {

    [SerializeField] private BarController bar;
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    public float CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            this.currentHp = Mathf.Clamp(value, 0, MaxHp);
        }
    }

    public float MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            this.maxHp = value;
            bar.MaxValue = maxHp;
        }
    }

    public void Initialize()
    {
        this.MaxHp = maxHp;
        this.CurrentHp = currentHp;
    }
}
