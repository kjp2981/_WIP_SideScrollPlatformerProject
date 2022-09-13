using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : TInventory<SkillDataSO>
{
    protected override void EditSlot(int idx, SkillDataSO value = null)
    {
        if(value != null)
            slots[idx].List = tList[idx];
        else
            slots[idx].List = null;
    }
}
