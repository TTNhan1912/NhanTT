using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Config/Data", fileName = "LevelConfigSO")]
public class CharacterData : GameConfig
{
    public List<DataCharacter> DataCharacter;

    public DataCharacter GetDataCharacter(int index)
    {
        return DataCharacter[index];
    }

}
[Serializable]
public class DataCharacter
{
    public string Name;
    public int Attack;
    public float Hp;
    public float Mp;
}
