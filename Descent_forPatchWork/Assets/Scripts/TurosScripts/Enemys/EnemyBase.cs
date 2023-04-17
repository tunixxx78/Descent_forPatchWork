using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBase
{
    public string EnemyName;
    public float enemyStrength, enemyHealth;
    public float enemyLevel;
    public int enemyShield;
    public int enemyMove;
    public int enemyType;

    public Sprite[] enemyImages;
    public bool thisEnemyIsAttacking;
}
