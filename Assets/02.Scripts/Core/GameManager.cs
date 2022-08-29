using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private PoolingListSO poolingList;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        PoolManager.Instance = new PoolManager(transform);

        CreatePool();
    }


    private void CreatePool()
    {
        foreach (PoolingPair pair in poolingList.list)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
        }
    }
}
