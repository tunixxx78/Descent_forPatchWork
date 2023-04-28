using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HeroBase
{
    public string plrName;
    public float plrHealth;
    public float plrStrength;
    public int plrLevel;
    public int plrShield;
    public int plrActionPoints;
    public int plrIndex;
    public int plrMove;

    public Sprite[] heroImages;

    public bool thisHeroIsAttacking;
    public bool thisHeroIsTakingDamageOnActivation, thisHeroIsTakingDamageOnActivation2, thisHeroIsGettingHealthOnActivation;

}
