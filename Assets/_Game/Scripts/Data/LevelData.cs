using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

[System.Serializable]
public class LevelSaveData
{

    #region PARAMS
    public int currentLevel;
    public int highestLevel;

    #endregion

    #region PROPERTIES
    public LevelSaveData(int currentLevelId, int highestLevelId)
    {
        this.currentLevel = currentLevelId;
        this.highestLevel = highestLevelId;
    }
    #endregion

}



public class LevelData : GameData
{

    #region EDITOR PARAMS
    [SerializeField]
    private LevelSaveData _levelSave;
    #endregion


    #region EVENTS

    public LevelSaveData LevelSave { get => _levelSave; set => _levelSave = value; }
    public int CurrentLevelId { get => this._levelSave.currentLevel; set => this._levelSave.currentLevel = value; }
    public int HighestLevelId { get => this._levelSave.highestLevel; set => this._levelSave.highestLevel = value; }
    #endregion


    #region METHODS

    public int GetCurrentLevel()
    {
        return this._levelSave.currentLevel;
    }

    public override void SaveData()
    {
        DataManager.Instance.SaveData<LevelSaveData>(GetType().FullName, _levelSave);
    }

    public override void LoadData()
    {
        _levelSave = DataManager.Instance.LoadData<LevelSaveData>(GetType().FullName);
    }

    public override void NewData()
    {
        _levelSave = new LevelSaveData(1, 0);
        SaveData();
    }

    public void PassLevel()
    {
        if (CurrentLevelId > HighestLevelId)
            HighestLevelId = CurrentLevelId;
        CurrentLevelId++;
        SaveData();
    }

    #endregion



   
}