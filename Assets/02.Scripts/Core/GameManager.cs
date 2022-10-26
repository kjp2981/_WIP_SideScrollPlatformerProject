using Cinemachine;
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

    private List<KeyCode> popupList = new List<KeyCode>();


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
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Time.timeScale != 0)
            {
                if (popupList.Count == 0)
                {
                    UIManager.Instance.SetInventoryActive(true);
                    TimeManager.Instance.ModifyTimeScale(0, 0);
                    playerVcam.gameObject.SetActive(true);

                    popupList.Add(KeyCode.B);
                }
            }
            else
            {
                if (popupList.Contains(KeyCode.B))
                {
                    UIManager.Instance.SetInventoryActive(false);
                    TimeManager.Instance.ModifyTimeScale(1, 0);
                    playerVcam.gameObject.SetActive(false);

                    popupList.Remove(KeyCode.B);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                if (popupList.Count == 0)
                {
                    UIManager.Instance.SetSettingPanelActive(true);
                    TimeManager.Instance.ModifyTimeScale(0, 0);

                    popupList.Add(KeyCode.Escape);
                }
            }
            else
            {
                if (popupList.Contains(KeyCode.Escape))
                {
                    UIManager.Instance.SetSettingPanelActive(false);
                    TimeManager.Instance.ModifyTimeScale(1, 0);

                    popupList.Remove(KeyCode.Escape);
                }
                else if(popupList.Count > 0 && popupList.Contains(KeyCode.Escape) == false)
                {

                }
            }
        }
    }

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
}
