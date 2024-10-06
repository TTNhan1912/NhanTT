using UnityEngine;
using System;
#if MOREMOUNTAINS_NICEVIBRATIONS_INSTALLED
using Lofelt.NiceVibrations;
#endif

namespace Framework
{
    public class VibrationManager : MonoSingleton<VibrationManager>
    {
        public enum EHapticType { Selection, Success, Warning, Failure, LightImpact, MediumImpact, HeavyImpact, RigidImpact, SoftImpact, None }

        public static event Action<bool> OnStatusChanged;
        public string SaveKey => "VibrationManager";
        public bool IsPlayerPrefsSave;

        #region PROPERTIES

        public bool IsVibrationOn
        {
            get
            {
                if (IsPlayerPrefsSave)
                {
                    return PlayerPrefs.GetInt(SaveKey, 1) == 1;
                }
                else
                {
                    return DataManager.Instance.GetData<UserData>().HasVibration();
                }
            }
            set
            {
                if (IsPlayerPrefsSave)
                {
                    PlayerPrefs.SetInt(SaveKey, value ? 1 : 0);
                }
                else
                {
                    DataManager.Instance.GetData<UserData>().SetVibration(value);
                }
            }
        }

        #endregion


        public void Haptic(EHapticType type)
        {
            if (!IsVibrationOn)
                return;


#if MOREMOUNTAINS_NICEVIBRATIONS_INSTALLED
            HapticPatterns.PlayPreset((HapticPatterns.PresetType)type);
#endif
        }


    }
}
