using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TInventory<T> : MonoBehaviour where T : ScriptableObject
{
    [SerializeField]
    protected int listCount = 45;

    [SerializeField]
    protected List<T> tList = new List<T>();

    [SerializeField]
    protected Transform slotParent;
    [SerializeField]
    protected TSlot<T>[] slots;

    protected T selectSlot;
    public T SelectSlot
    {
        get => selectSlot;
        set => selectSlot = value;
    }

    protected void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<TSlot<T>>();

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].Id = i;
        }
    }

    protected void Awake()
    {
        for (int i = 0; i < listCount; i++)
        {
            Slash slot = PoolManager.Instance.Pop("Slot") as Slash;
            slot.transform.parent = slotParent;
            slot.transform.position = Vector3.zero;
            slot.GetComponentInChildren<TSlot<T>>().SetParentInventory(this);
        }

        FreshSlot();
    }

    public void AddSlot()
    {
        slots = slotParent.GetComponentsInChildren<TSlot<T>>();
    }

    public void FreshSlot()
    {
        if(slots.Length == 0)
        {
            AddSlot();
        }

        int i = 0;
        for(; i < tList.Count && i < slots.Length; i++)
        {
            EditSlot(i, tList[i]);
        }
        for(; i < slots.Length; i++)
        {
            EditSlot(i, null);
        }
    }

    protected abstract void EditSlot(int idx, T value = null);

    public void AddItem(T value)
    {
        if(tList.Count < slots.Length)
        {
            tList.Add(value);
            FreshSlot();
        }
        else
        {
            Debug.LogWarning("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }

    public T GetIndexOfData(int index)
    {
        T data = tList[index];
        return data;
    }
}
