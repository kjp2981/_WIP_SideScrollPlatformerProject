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

    private bool isPopup = false;

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
                isPopup = true;
                UIManager.Instance.SetInventoryActive(true);
                TimeManager.Instance.ModifyTimeScale(0, 0);
                playerVcam.gameObject.SetActive(true);
            }
            else
            {
                isPopup = false;
                UIManager.Instance.SetInventoryActive(false);
                TimeManager.Instance.ModifyTimeScale(1, 0);
                playerVcam.gameObject.SetActive(false);
            }
        }

        if (isPopup != true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale != 0)
                {
                    TimeManager.Instance.ModifyTimeScale(0, 0);
                }
                else
                {
                    TimeManager.Instance.ModifyTimeScale(1, 0);
                }
            }
        }
    }
}
