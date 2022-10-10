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

    public void GetItem(int count = 5)
    {
        Item item = null;
        for(int i = 0; i < count; i++)
        {
            Item dropItem = ItemDrop();
            if (dropItem != null)
            {
                item = PoolManager.Instance.Pop(dropItem.name) as Item;
                item.transform.position = this.transform.position;
                item.Scattering();
            }
        }
    }
}
