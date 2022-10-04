using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : TSlot<SkillDataSO>
{
    public override void Reset()
    {
        
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
        SkillDataSO data = List;
        if(data != null)
        {
            UIManager.Instance.SetActiveSkillPanel(true);
            UIManager.Instance.SkillDescriptionPanel(data);
            parentInventory.SelectSlot = data;
            parentInventory.SelectID = Id;
        }
        else
        {
            UIManager.Instance.SetActiveSkillPanel(false);
            UIManager.Instance.SkillDescriptionPanel(data);
            parentInventory.SelectSlot = data;
            parentInventory.SelectID = Id;
        }
        
    }
}
