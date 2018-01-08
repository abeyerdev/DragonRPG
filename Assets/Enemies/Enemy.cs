using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float currentHP = 100f;
    [SerializeField]
    float maxHP = 100f;

    public float HealthAsPercentage
    {
        get
        {
            return currentHP / maxHP;
        }
    }
}
