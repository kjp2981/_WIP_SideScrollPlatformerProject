using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : TInventory<WeaponStatusDataSO>
{
    protected override void Awake()
    {
        for (int i = 0; i < listCount; i++)
        {
            Slash slot = PoolManager.Instance.Pop("WeaponSlot") as Slash;
            slot.transform.parent = slotParent;
            slot.transform.position = Vector3.zero;
            WeaponSlot tslot = slot.GetComponentInChildren<WeaponSlot>();
            tslot.Id = i;
            tslot.SetParentInventory(this);
        }

        FreshSlot();
    }

    protected override void EditSlot(int idx, WeaponStatusDataSO value = null)
    {
        if (value != null)
            slots[idx].List = tList[idx];
        else
            slots[idx].List = null;
    }

    public void SetActiveUseText(bool active)
    {
        WeaponSlot beforeSlot = slots[beforeId] as WeaponSlot;
        beforeSlot.IsUse = false;

        WeaponSlot slot = slots[selectId] as WeaponSlot;
        slot.IsUse = active;
    }

    public bool ReturnActiveSelectUseText()
    {
        WeaponSlot slot = slots[selectId] as WeaponSlot;
        return slot.IsUse;
    }
}
