using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : Item
{
    public override void UseItem()
    {
        Debug.Log("Player Heal!");
    }
}
