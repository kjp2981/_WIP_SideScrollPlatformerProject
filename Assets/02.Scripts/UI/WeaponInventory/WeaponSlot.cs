using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : TSlot<WeaponStatusDataSO>
{
    private WeaponInfo weaponInfo;

    [SerializeField]
    private Text useText;

    private bool isUse = false;
    public bool IsUse
    {
        get => isUse;
        set
        {
            isUse = value;
            SetActiveUseText(IsUse);
        }
    }

    private void Start()
    {
        weaponInfo = GetComponent<WeaponInfo>();
    }

    public override void Reset()
    {

    }

    private void SetActiveUseText(bool isUse)
    {
        useText.gameObject.SetActive(isUse);
    }

    protected override void IsNotValue()
    {
        image.sprite = list.image;
        image.color = new Color(1, 1, 1, 1);
    }

    protected override void IsValue()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    public override void OnClickEvent()
    {
        WeaponStatusDataSO data = List;
        if(data != null)
        {
            UIManager.Instance.SetActiveWeaponDescriptionPanel(true);
            UIManager.Instance.WeaponDescriptionPanel(data);
            parentInventory.SelectSlot = data;
            //weaponInfo.Inventory.Slots[weaponInfo.Inventory.SelectID].is // 어기서 전 ID의 IsUse를 False로 바꾸어야한다.
            parentInventory.SelectID = Id;
        }
        else
        {
            UIManager.Instance.SetActiveWeaponDescriptionPanel(false);
            UIManager.Instance.WeaponDescriptionPanel(data);
            parentInventory.SelectSlot = data;
            parentInventory.SelectID = Id;
        }
    }
}
