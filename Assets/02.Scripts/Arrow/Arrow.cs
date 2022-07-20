using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolableMono
{
    public override void Reset()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PoolManager.Instance.Push(this);
    }
}
