using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TInventory<T> : MonoBehaviour where T : ScriptableObject
{
    [SerializeField]
    protected List<T> tList = new List<T>();

    [SerializeField]
    protected Transform slotParent;
    [SerializeField]
    protected TSlot<T>[] slots;

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
        FreshSlot();
    }

    public void FreshSlot()
    {
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

    public SkillDataSO GetIndexOfSkillData(int index)
    {
        
    }
}
