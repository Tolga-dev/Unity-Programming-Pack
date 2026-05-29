using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData enemyData;
    public float health;
    public float attack = 10f;

    private void Start()
    {
        health = enemyData._maxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        health--;
        Debug.Log(health);
    }
}
