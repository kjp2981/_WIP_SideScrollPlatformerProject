using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : TSlot<WeaponStatusDataSO>
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
        // ��� ����
        // 1. ���� ���� Ÿ�� �Ǵ� �� ��ġ�� �´� ���� �����
        // 2. UI ���� �κ��丮���� �� ��� E���� ����(���ʹ��� �������)
    }
}
