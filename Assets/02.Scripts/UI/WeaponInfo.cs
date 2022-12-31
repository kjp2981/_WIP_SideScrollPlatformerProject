using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WeaponInfo : MonoBehaviour
{
    private Dictionary<WeaponType, WeaponStatusDataSO> weaponDataDic = new Dictionary<WeaponType, WeaponStatusDataSO>();
    public Dictionary<WeaponType, WeaponStatusDataSO> WeaponDataDic => weaponDataDic;

    [SerializeField, BoxGroup("Default Image")]
    private Sprite[] _defaultImage;
    private Dictionary<WeaponType, Sprite> spriteDic = new Dictionary<WeaponType, Sprite>();
    public Dictionary<WeaponType, Sprite> SpriteDic => spriteDic;

    private List<WeaponImage> weaponImage = new List<WeaponImage>();
    private WeaponInventory inventory;
    public WeaponInventory Inventory => inventory;

    [SerializeField]
    private SkillCollection skillCollection;

    private void Start()
    {
        spriteDic.Add(WeaponType.Sword, _defaultImage[(int)WeaponType.Sword]);
        spriteDic.Add(WeaponType.Spear, _defaultImage[(int)WeaponType.Spear]);
        spriteDic.Add(WeaponType.Wand, _defaultImage[(int)WeaponType.Wand]);
        spriteDic.Add(WeaponType.Bow, _defaultImage[(int)WeaponType.Bow]);
        spriteDic.Add(WeaponType.Shield, _defaultImage[(int)WeaponType.Shield]);
        spriteDic.Add(WeaponType.Auxiliary, _defaultImage[(int)WeaponType.Auxiliary]);
        spriteDic.Add(WeaponType.Auxiliary2 , _defaultImage[(int)WeaponType.Auxiliary2]);

        for(int i = 0; i < (int)WeaponType.COUNT; i++)
        {
            weaponDataDic.Add((WeaponType)i, null);
        }

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
                    if(weaponDataDic[wi.Type] != null)
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

        if(weapon.weaponType == WeaponType.Auxiliary)
        {
            skillCollection.SetWeaponSkill();
        }

        UpdateImage(weapon.weaponType);
    }

    public void SubtractionWeapon()
    {
        SubtractionWeapon(inventory.SelectSlot);
    }

    public void SubtractionWeapon(WeaponStatusDataSO weapon, int index)
    {
        if (weaponDataDic.ContainsKey(weapon.weaponType))
        {
            weaponDataDic[weapon.weaponType] = null;
        }

        if (weapon.weaponType == WeaponType.Auxiliary)
        {
            skillCollection.UnSetWeaponSkill(index);
        }

        UpdateImage(weapon.weaponType);
    }
}
