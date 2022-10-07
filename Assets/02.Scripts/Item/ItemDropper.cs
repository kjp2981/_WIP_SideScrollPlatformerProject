using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPair
{
    public Item item;
    public int priority;
}

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ItemPair> itemList = new List<ItemPair>();

    public Item ItemDrop()
    {
        int sum = 0;
        foreach(ItemPair pair in itemList)
        {
            sum += pair.priority;
        }

        int random = Random.Range(0, sum + 1);
        int min = 0;
        for(int i = 0; i < itemList.Count; i++)
        {
            if(random > min && random <= itemList[i].priority)
            {
                return itemList[i].item;
            }
            else
            {
                min += itemList[i].priority;
            }
        }

        return null;
    }
}
