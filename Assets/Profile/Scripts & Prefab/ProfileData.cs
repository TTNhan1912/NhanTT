using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProfileData : MonoBehaviour
{
    public UserProfile User;

    const string SAVE = "profile_data";
    public Action OnObtainedExperiencePoint;

    void Awake()
    {
        Load();
    }

    public void SetName(string name)
    {
        User.Name = string.IsNullOrEmpty(name) ? "You" : name;
        Save();
    }

    public void SetAvatar(int index)
    {
        User.AvatarIndex = index;
        Save();
    }

    public int GetRequirementExperiencePoint()
    {
        //Xử lý mechanic để tính điểm kinh nghiệm cần để lên cấp
        return User.Level * 1000;
    }

    public void ObtainExperiencePoint(int point)
    {
        User.ExperiencePoint += point;
        while (User.ExperiencePoint >= GetRequirementExperiencePoint()) LevelUp();
        Save();

        OnObtainedExperiencePoint?.Invoke();
    }

    public void LevelUpDirectly()
    {
        LevelUp();
        Save();
    }

    void LevelUp()
    {
        User.ExperiencePoint -= GetRequirementExperiencePoint();
        User.Level++;
    }

    void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE))
        {
            Save();
            return;
        }
        else
        {
            User = JsonUtility.FromJson<UserProfile>(PlayerPrefs.GetString(SAVE));
        }
    }

    void Save()
    {
        PlayerPrefs.SetString(SAVE, JsonUtility.ToJson(User));
    }
}

[Serializable]
public class UserProfile
{
    public string Name = "You";
    public int Level = 1;
    public int ExperiencePoint;
    public int AvatarIndex;

    public UserProfile(UserProfile userProfile)
    {
        Name = userProfile.Name;
        Level = userProfile.Level;
        ExperiencePoint = userProfile.ExperiencePoint;
        AvatarIndex = userProfile.AvatarIndex;
    }
}
