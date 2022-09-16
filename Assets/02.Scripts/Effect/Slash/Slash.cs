using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : PoolableMono
{
    public override void Reset()
    {
        
    }

    public void Pooling()
    {
        PoolManager.Instance.Push(this);
    }
}
