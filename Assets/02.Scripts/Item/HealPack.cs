using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class HealPack : Item
{
    private IRecovery recovery;

    [SerializeField]
    private int healValue = 5;

    private void Start()
    {
        recovery = Define.Player.GetComponent<IRecovery>();
    }

    public override void UseItem()
    {
        recovery.Heal(healValue);
    }
}
