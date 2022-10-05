using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private PoolingListSO poolingList;

    [SerializeField]
    private CinemachineVirtualCamera playerVcam;

    private bool isTimeStop = false;

    void Awake()
    {
        if (Instance == null)
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Time.timeScale != 0)
            {
                if (isTimeStop != true)
                {
                    isTimeStop = true;
                    UIManager.Instance.SetInventoryActive(true);
                    TimeManager.Instance.ModifyTimeScale(0, 0);
                    playerVcam.gameObject.SetActive(true);
                }
            }
            else
            {
                isTimeStop = false;
                UIManager.Instance.SetInventoryActive(false);
                TimeManager.Instance.ModifyTimeScale(1, 0);
                playerVcam.gameObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                if (isTimeStop != true)
                {
                    isTimeStop = true;
                    TimeManager.Instance.ModifyTimeScale(0, 0);
                }
            }
            else
            {
                isTimeStop = false;
                TimeManager.Instance.ModifyTimeScale(1, 0);
            }
        }
    }
}
