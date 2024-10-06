using System;
using UnityEngine;

[Serializable]
public class UserSaveData
{
    public bool IsNoAd;
    public DateTime StartTime;
    public string StartTimeString;

    public bool IsVibrationOn;
    public bool IsMusicOn;
    public bool IsSoundOn;
}

public class UserData : GameData
{
    [SerializeField] private UserSaveData _userSaveDataSave;

    public UserSaveData UserSaveDataSave
    {
        get => _userSaveDataSave;
        set => _userSaveDataSave = value;
    }
    
    public override void SaveData()
    {
        DataManager.Instance.SaveData<UserSaveData>(GetName(), UserSaveDataSave);
    }

    public override void LoadData()
    {
        UserSaveDataSave = DataManager.Instance.LoadData<UserSaveData>(GetName());
    }

    public override void NewData()
    {
        UserSaveDataSave = new UserSaveData();
        UserSaveDataSave.IsNoAd = false;
        UserSaveDataSave.StartTime = DateTime.Now;
        UserSaveDataSave.StartTimeString = UserSaveDataSave.StartTime.ToString();

        UserSaveDataSave.IsVibrationOn = true;
        UserSaveDataSave.IsMusicOn = true;
        UserSaveDataSave.IsSoundOn = true;
    }
    
    public bool HasNoAd()
    {
        return UserSaveDataSave.IsNoAd;
    }

    public void SetNoAd(bool IsNoAd)
    {
        UserSaveDataSave.IsNoAd = IsNoAd;
        SaveData();
    }

    public bool HasMusic()
    {
        return UserSaveDataSave.IsMusicOn;
    }

    public void SetMusic(bool isOn)
    {
        UserSaveDataSave.IsMusicOn = isOn;
        SaveData();
    }

    public bool HasSound()
    {
        return UserSaveDataSave.IsSoundOn;
    }


    public void SetSound(bool isOn)
    {
        UserSaveDataSave.IsSoundOn = isOn;
        SaveData();
    }

    public bool HasVibration()
    {
        return UserSaveDataSave.IsVibrationOn;
    }

    public void SetVibration(bool isOn)
    {
        UserSaveDataSave.IsVibrationOn = isOn;
        SaveData();
    }
}