using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TSlot<T> : PoolableMono where T : ScriptableObject
{
    [SerializeField] protected Image image;

    protected T list;
    public T List
    {
        get => list;
        set
        {
            list = value;
            if(list != null)
            {
                IsNotValue();
            }
            else
            {
                IsValue();
            }
        }
    }

    protected abstract void IsValue();

    protected abstract void IsNotValue();
}