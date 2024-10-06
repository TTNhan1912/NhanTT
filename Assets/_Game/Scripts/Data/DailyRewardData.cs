using System;

public class DailyRewardData : GameData
{
    #region OVERRIDES

    private DailyRewardSaveData _saveData;

    public DailyRewardSaveData GetSaveData()
    {
        return _saveData;
    }
    
    public override void SaveData()
    {
        DataManager.Instance.SaveData(GetType().FullName, _saveData);
    }

    public override void LoadData()
    {
        _saveData = DataManager.Instance.LoadData<DailyRewardSaveData>(GetType().FullName);
    }

    public override void NewData()
    {
        SaveData();
    }

    #endregion // OVERRIDES
}

[Serializable]
public class DailyRewardSaveData
{
    public float DayLength = 86400; // 24h

    // Daily reward logic.
    public string LastRewardClaimedTime;
    public int CurrentRewardDay = 1;

    // In case you need it later.
    public string FirstLoginTime;
    public int DayLoginCount = 1;
    public string LastLoginTime;
}
