using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Config/Level", fileName = "LevelConfigSO")]
public class LevelConfigSO : GameConfig
{
    public List<Level> Levels = new List<Level>();

    public Level GetLevel(int levelIndex)
    {
        return Levels[(levelIndex - 1) % Levels.Count];
    }


    #region DEBUG

#if UNITY_EDITOR
    // private void OnValidate()
    // {
    //     bonusIndexList.Clear();
    //     normalIndexList.Clear();
    //     foreach (var item in levelsList)
    //     {
    //         if (item.isBonusLevel)
    //         {
    //             bonusIndexList.Add(levelsList.IndexOf(item) + 1);
    //         }
    //         else
    //         {
    //             normalIndexList.Add(levelsList.IndexOf(item) + 1);
    //         }
    //     }
    // }
#endif

    #endregion
}
