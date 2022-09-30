using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WeaponInfo : MonoBehaviour
{
    private Dictionary<WeaponType, WeaponStatusDataSO> weaponDataDic = new Dictionary<WeaponType, WeaponStatusDataSO>();

    [SerializeField, BoxGroup("Default Image")]
    private Sprite swordDefaultImage, spearDefaultImage, wandDefaultImage, bowDefaultImage, shieldDefaultImage, abilityDefaultImage;

    public void AddWeapon(WeaponStatusDataSO weapon)
    {
        if (weaponDataDic.ContainsKey(weapon.weaponType))
        {
            weaponDataDic[weapon.weaponType] = weapon;
        }
        else
        {
            weaponDataDic.Add(weapon.weaponType, weapon);
        }
    }

    public void SubtractionWeapon(WeaponStatusDataSO weapon)
    {
        if (weaponDataDic.ContainsKey(weapon.weaponType))
        {
            weaponDataDic[weapon.weaponType] = null;
        }
    }
}
