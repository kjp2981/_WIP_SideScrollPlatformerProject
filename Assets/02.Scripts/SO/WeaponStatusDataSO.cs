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

public class WeaponStatusDataSO : ScriptableObject
{
    public new string name;
    [ShowAssetPreview(32, 32)]
    public Sprite image;
    [ResizableTextArea]
    public string description;

    public WeaponType weaponType;
    public AbilityType abilityType;
    public float mutiplyValue;
}
