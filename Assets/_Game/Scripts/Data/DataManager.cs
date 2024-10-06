using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Framework;
using UnityEngine;
using UnityEngine.Events;

public enum ESaveType
{
    PlayerPrefs,
    File
}

public class DataManager : MonoSingleton<DataManager>
{
    public ESaveType SaveType;
    private  string _savePath => PathUtils.GetDataPath() + "/Save";

    public List<GameConfig> GameConfigsList = new List<GameConfig>();
    public List<GameData> GameDatasList = new List<GameData>();


    [ButtonMethod]
    [ContextMenu("DeleteSave")]
    public void DeleteSave()
    {
        switch (SaveType)
        {
            case ESaveType.PlayerPrefs:
                PlayerPrefs.DeleteAll();
                break;
            case ESaveType.File:
                try
                {
                    if (Directory.Exists(_savePath))
                    {
                        Debug.Log("Folder exists. Deleting the folder...");

                        // Delete the folder and its contents
                        Directory.Delete(_savePath, true);

                        Debug.Log("Folder deleted.");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
                break;
        }
       
    }


    private void CreateFolder()
    {
        if (SaveType == ESaveType.PlayerPrefs)
            return;

        if (Directory.Exists(_savePath)) return;

        Directory.CreateDirectory(_savePath);
        Debug.Log("save path : " + _savePath);
    }

    
    protected override void Initiate()
    {
        base.Initiate();
        CreateFolder();


        for (int i = 0; i < GameDatasList.Count; i++)
        {
            var data = GameDatasList[i];
            data.LoadData();
            if (!data.HasData())
            {
                data.NewData();
            }
            else if (data.HasUpdateData())
            {
                data.UpdateData();
                Debug.LogErrorFormat("{0} has Update!!", data.GetName());
            }
            data.Initiate();
        }
    }


    public T GetConfig<T>() where T : ScriptableObject
    {
        try
        {
            return GameConfigsList.Find(x => x.GetType().FullName == typeof(T).FullName) as T;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing ScriptableObject: {0}", typeof(T).FullName);
            return null;
        }
    }


    public T GetData<T>() where T : GameData
    {
        try
        {
            return GameDatasList.Find(x => x.GetType().FullName == typeof(T).FullName) as T;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing GameData: {0}", typeof(T).FullName);
            return null;
        }
    }


    #region SAVE

    public void SaveData<T>(string key, T userSaveData)
    {
        string JsonDataEncode = JsonUtility.ToJson(userSaveData, false);
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(JsonDataEncode);
        string encodedText = Convert.ToBase64String(bytesToEncode);

        switch (SaveType)
        {
            case ESaveType.PlayerPrefs:
                SaveDataToPlayerPrefs(key, encodedText);
                break;
            case ESaveType.File:
                SaveDataToFile(key, bytesToEncode);
                break;
            default:
                SaveDataToPlayerPrefs(key, encodedText);
                break;
        }


    }


    private void SaveDataToPlayerPrefs(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    private void SaveDataToFile(string key, byte[] data)
    {
        var savePath = _savePath + $"/{key}";

        SimpleEncrypt(ref data);
        File.WriteAllBytes(savePath, data);
    }

    private void SimpleEncrypt(ref byte[] data)
    {
        if (Application.isEditor)
            return;

        byte[] key = System.Text.Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier);
        int k_len = key.Length;
        for (uint i = 0; i < data.Length; i++)
            data[i] ^= key[i % k_len];
    }

    #endregion


    #region LOAD

    public T LoadData<T>(string key)
    {
        switch (SaveType)
        {
            case ESaveType.PlayerPrefs:
                return LoadDataFromPlayerPrefs<T>(key);
            case ESaveType.File:
                return LoadDataFromFile<T>(key);
            default:
                return LoadDataFromPlayerPrefs<T>(key);
        }
    }

    private T LoadDataFromPlayerPrefs<T>(string key)
    {
        string jsonData = PlayerPrefs.GetString(key);
        byte[] decodedBytes = Convert.FromBase64String(jsonData);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        T _d = JsonUtility.FromJson<T>(decodedText);
        return _d;
    }


    private T LoadDataFromFile<T>(string key) 
    {
        var savePath = _savePath + $"/{key}";
        var data = File.Exists(savePath) ? File.ReadAllBytes(savePath) : Array.Empty<byte>();
        string decodedText = Encoding.UTF8.GetString(data);
        T _d = JsonUtility.FromJson<T>(decodedText);
        return _d;
    }

    #endregion


    public bool HasData(string key)
    {
        return SaveType switch
        {
            ESaveType.PlayerPrefs => PlayerPrefs.HasKey(key),
            ESaveType.File => File.Exists(_savePath + $"/{key}")
        };
    }


    public void LoadAllData()
    {
        foreach (GameData data in GameDatasList)
        {
            data.LoadData();
        }
    }


    public void SaveAllData()
    {
        foreach (GameData data in GameDatasList)
        {
            data.SaveData();
        }
    }


    public void DeleteAllData()
    {
        foreach (GameData data in GameDatasList)
        {
            data.NewData();
        }
    }


    private void OnApplicationQuit()
    {
        SaveAllData();
    }


    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllData();
        }
    }

    #region HELPERS

#if UNITY_EDITOR
    private void OnValidate()
    {
        GameConfigsList.Clear();
        GameConfigsList.AddRange(Resources.FindObjectsOfTypeAll<GameConfig>());

        GameDatasList.Clear();
        GetComponents<GameData>(GameDatasList);
    }
#endif

    #endregion

}
