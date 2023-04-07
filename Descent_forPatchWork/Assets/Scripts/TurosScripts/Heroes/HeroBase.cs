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
    public int plrIndex;

    public Sprite[] heroImages;

    public bool thisHeroIsAttacking;

}
