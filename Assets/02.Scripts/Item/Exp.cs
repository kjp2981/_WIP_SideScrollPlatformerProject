using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Exp : Item
{
    private LevelController playerLevel;

    [SerializeField]
    private int exp;

    private void Start()
    {
        playerLevel = Define.Player.GetComponent<LevelController>();
    }

    public override void UseItem()
    {
        playerLevel.AddExp(exp);

        PoolManager.Instance.Push(this);
    }
}
