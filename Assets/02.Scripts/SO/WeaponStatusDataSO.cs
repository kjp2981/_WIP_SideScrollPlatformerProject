using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum WeaponType
{
    Sword,
    Spear,
    Wand,
    Bow,
    Shield,
    Auxiliary
}

public enum AbilityType
{
    HP,
    MeleeDamage,
    RangeDamage,
    Defence,
    CriticalRate,
    CriticalDamage
}

[CreateAssetMenu(menuName = "SO/Agent/Weapon")]
public class WeaponStatusDataSO : ScriptableObject
{
    public new string name;
    [ShowAssetPreview(32, 32)]
    public Sprite image;
    [ResizableTextArea]
    public string description;

    public WeaponType weaponType;
    public AbilityType abilityType;
    [Min(0)]
    public int addValue;

    public SkillDataSO skill;
}
