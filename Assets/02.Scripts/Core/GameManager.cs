using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private PoolingListSO poolingList;

    [SerializeField]
    private CinemachineVirtualCamera playerVcam;

    private Action<bool> PopupPanelActive;
    private bool isPopup = false;

    #region SAVE_DATA
    private string PATH = "";
    private readonly string FILE_NAME = "SaveData.txt";

    private PlayerSaveData saveData;
    #endregion


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        PATH = Application.dataPath + "/SAVE_DATA_FILE";
        if (!Directory.Exists(PATH))
            Directory.CreateDirectory(PATH);

        saveData = LoadJsonFile<PlayerSaveData>(PATH, FILE_NAME);

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
        InputAllKey();
    }

    #region Popup Panel Setting
    void SettingPanel(bool value)
    {
        UIManager.Instance.SetSettingPanelActive(value);
    }

    void InventoryPanel(bool value)
    {
        UIManager.Instance.SetInventoryActive(value);
        playerVcam.gameObject.SetActive(value);
    }
    #endregion

    void InputAllKey()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InputKey(KeyCode.B);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputKey(KeyCode.Escape);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InputKey(KeyCode.G);
        }
    }

    void InputKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Escape:
                if(PopupPanelActive == null)
                    PopupPanelActive = SettingPanel;
                break;
            case KeyCode.B:
                if (PopupPanelActive == null)
                    PopupPanelActive = InventoryPanel;
                else if (PopupPanelActive != InventoryPanel)
                    return;
                break;
            default:
                return;
        }

        if (Time.timeScale != 0)
        {
            isPopup = true;
            PopupPanelActive?.Invoke(true);
            TimeManager.Instance.ModifyTimeScale(0, 0);
        }
        else
        {
            isPopup = false;
            PopupPanelActive?.Invoke(false);
            TimeManager.Instance.ModifyTimeScale(1, 0);

            PopupPanelActive = null;
        }
    }


    #region Save&Load
    public void SaveJson<T>(string createPath, string fileName, T value)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        string json = JsonUtility.ToJson(value, true);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName) where T : new()
    {
        if (File.Exists(string.Format("{0}/{1}.json", loadPath, fileName)))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        SaveJson<T>(loadPath, fileName, new T());
        return LoadJsonFile<T>(loadPath, fileName);
    }
    #endregion
}
