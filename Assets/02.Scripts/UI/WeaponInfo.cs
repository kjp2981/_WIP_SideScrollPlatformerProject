using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WeaponInfo : MonoBehaviour
{
    private Dictionary<WeaponType, WeaponStatusDataSO> weaponDataDic = new Dictionary<WeaponType, WeaponStatusDataSO>();

    [SerializeField, BoxGroup("Default Image")]
    private Sprite swordDefaultImage, spearDefaultImage, wandDefaultImage, bowDefaultImage, shieldDefaultImage, abilityDefaultImage;
    private Dictionary<WeaponType, Sprite> spriteDic = new Dictionary<WeaponType, Sprite>();
    public Dictionary<WeaponType, Sprite> SpriteDic => spriteDic;

    private List<WeaponImage> weaponImage = new List<WeaponImage>();
    private WeaponInventory inventory;

    private void Start()
    {
        spriteDic.Add(WeaponType.Sword, swordDefaultImage);
        spriteDic.Add(WeaponType.Spear, spearDefaultImage);
        spriteDic.Add(WeaponType.Wand, wandDefaultImage);
        spriteDic.Add(WeaponType.Bow, bowDefaultImage);
        spriteDic.Add(WeaponType.Shield, shieldDefaultImage);
        spriteDic.Add(WeaponType.Auxiliary, abilityDefaultImage);

        inventory = GetComponentInParent<WeaponInventory>();

        GetComponentsInChildren<WeaponImage>(weaponImage);

        UpdateImage();
    }

    public void UpdateImage(WeaponType? type = null)
    {
        if (type.HasValue == false)
        {

            foreach (WeaponImage wi in weaponImage)
            {
                if(weaponDataDic.ContainsKey(wi.Type))
                    wi.ChangeItemImage(weaponDataDic[wi.Type].image);
            }
        }
        else
        {
            foreach (WeaponImage wi in weaponImage)
            {
                if (wi.Type == type)
                {
                    wi.ChangeItemImage(weaponDataDic[wi.Type].image);
                    break;
                }
                else continue;
            }
        }
    }

    public void AddWeapon()
    {
        AddWeapon(inventory.SelectSlot);
    }

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
        UpdateImage(weapon.weaponType);
    }

    public void SubtractionWeapon()
    {
        SubtractionWeapon(inventory.SelectSlot);
    }

    public void SubtractionWeapon(WeaponStatusDataSO weapon)
    {
        if (weaponDataDic.ContainsKey(weapon.weaponType))
        {
            weaponDataDic[weapon.weaponType] = null;
        }
        UpdateImage(weapon.weaponType);
    }
}
