using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TSlot<T> : PoolableMono where T : ScriptableObject
{
    protected TInventory<T> parentInventory;

    protected virtual void Awake()
    {
        //parentInventory = transform.parent.parent.GetComponent<TInventory<T>>();
        parentInventory = transform.GetComponentInParent<TInventory<T>>();
    }

    private int id;
    public int Id
    {
        get => id;
        set => id = value;
    }

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

    public abstract void OnClickEvent();
}
