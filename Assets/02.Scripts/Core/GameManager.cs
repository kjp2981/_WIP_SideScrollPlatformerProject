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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                UIManager.Instance.SetInventoryActive(true);
                TimeManager.Instance.ModifyTimeScale(0, 0);
            }
            else
            {
                UIManager.Instance.SetInventoryActive(false);
                TimeManager.Instance.ModifyTimeScale(1, 0);
            }
        }
    }
}
