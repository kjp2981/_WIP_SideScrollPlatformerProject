using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Exp : Item
{
    public override void UseItem()
    {
        PoolManager.Instance.Push(this);
    }
}
