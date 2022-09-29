using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> inventoryList = new List<GameObject>();

    [ShowNonSerializedField]
    private int currentPage = 0;

    private void Awake()
    {
        RefreshList();

        SetActiveTrueCurrentPage();
    }

    private void RefreshList()
    {
        Clear();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            inventoryList.Add(this.transform.GetChild(i).gameObject);
        }

        AllActiveFalse();
    }

    private void Clear()
    {
        for(int i = 0; i < inventoryList.Count; i++)
        {
            inventoryList.RemoveAt(0);
        }
    }

    public void NextPage()
    {
        if (inventoryList.Count <= 1) return;

        SetActiveFalseCurrentPage();
        currentPage = (currentPage + 1) % inventoryList.Count;
        SetActiveTrueCurrentPage();
    }

    public void BeforePage()
    {
        if (inventoryList.Count <= 1) return;

        SetActiveFalseCurrentPage();
        currentPage = (currentPage - 1 < 0 ? inventoryList.Count - 1 : currentPage - 1) % inventoryList.Count;
        SetActiveTrueCurrentPage();
    }

    private void AllActiveFalse()
    {
        for(int i = 0; i < inventoryList.Count; i++)
        {
            inventoryList[i].SetActive(false);
        }
    }

    private void SetActiveFalseCurrentPage()
    {
        inventoryList[currentPage].SetActive(false);
    }

    private void SetActiveTrueCurrentPage()
    {
        inventoryList[currentPage].SetActive(true);
    }
}
