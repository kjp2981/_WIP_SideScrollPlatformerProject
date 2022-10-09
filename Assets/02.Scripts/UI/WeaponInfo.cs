using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WeaponInfo : MonoBehaviour
{
    private Dictionary<WeaponType, WeaponStatusDataSO> weaponDataDic = new Dictionary<WeaponType, WeaponStatusDataSO>();
    public Dictionary<WeaponType, WeaponStatusDataSO> WeaponDataDic => weaponDataDic;

    [SerializeField, BoxGroup("Default Image")]
    private Sprite swordDefaultImage, spearDefaultImage, wandDefaultImage, bowDefaultImage, shieldDefaultImage, abilityDefaultImage;
    private Dictionary<WeaponType, Sprite> spriteDic = new Dictionary<WeaponType, Sprite>();
    public Dictionary<WeaponType, Sprite> SpriteDic => spriteDic;

    private List<WeaponImage> weaponImage = new List<WeaponImage>();
    private WeaponInventory inventory;
    public WeaponInventory Inventory => inventory;

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
                    wi.ChangeItemImage(weaponDataDic[wi.Type].image, Color.white);
            }
        }
        else
        {
            foreach (WeaponImage wi in weaponImage)
            {
                if (wi.Type == type)
                {
                    if (weaponDataDic[wi.Type] != null)
                        wi.ChangeItemImage(weaponDataDic[wi.Type].image, Color.white);
                    else
                        wi.ChangeItemImage(spriteDic[wi.Type], new Color(1, 1, 1, 0.3f));
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

        // 여기서 피통을 늘려준다던가하는 거를 해야한다.
        
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
