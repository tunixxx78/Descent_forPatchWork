using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillCard")]
public class SkillCard : ScriptableObject
{
    public int initiativeValue;
    public int atk;
    public int heal;
    public string info;
    public int skillId;
    public bool closeRange, longRange;
    public Sprite Sprite;
}
