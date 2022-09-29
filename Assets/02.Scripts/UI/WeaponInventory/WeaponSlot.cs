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
        // 장비 장착
        // 1. 현재 무기 타입 판단 후 위치에 맞는 곳에 끼우기
        // 2. UI 갱신 인벤토리에도 이 장비에 E글자 띄우기(드루와던전 장비착용)
    }
}
