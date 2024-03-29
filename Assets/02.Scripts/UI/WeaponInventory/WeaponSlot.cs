using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : TSlot<WeaponStatusDataSO>
{
    private WeaponInfo weaponInfo;

    [SerializeField]
    private Text useText;


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
            parentInventory.SelectSlot = data;
            parentInventory.SelectID = Id;
            UIManager.Instance.WeaponDescriptionPanel(data);
        }
        else
        {
            UIManager.Instance.SetActiveWeaponDescriptionPanel(false);
            parentInventory.SelectSlot = data;
            parentInventory.SelectID = Id;
            UIManager.Instance.WeaponDescriptionPanel(data);
        }
    }
}
