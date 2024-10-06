using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [Header("--- REFERENCES ---")]
    [SerializeField] RectTransform avatarSelectionRect;
    [SerializeField] Image avatarImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_InputField nameField;

    [SerializeField] Slider experienceBar;
    [SerializeField] AvatarItem avatarItem;
    [SerializeField] ProfileData profileData;

    [SerializeField] Sprite[] avatarList;

    void Init()
    {
        avatarImage.sprite = avatarList[profileData.User.AvatarIndex];
        levelText.text = $"Level {profileData.User.Level}";
        nameField.text = $"{profileData.User.Name}";
        experienceBar.value = (float)profileData.User.ExperiencePoint / profileData.GetRequirementExperiencePoint();

        nameField.onEndEdit.AddListener(profileData.SetName);
        profileData.OnObtainedExperiencePoint += OnObtainedExperiencePoint;
        CreateAvatarItems();
    }

    void CreateAvatarItems()
    {
        for (int i = 0; i < avatarList.Length; i++)
        {
            Instantiate(avatarItem, avatarSelectionRect).Init(avatarList[i], i, (id) =>
            {
                OnAvatarSelectionClick();
                profileData.SetAvatar(id);
                avatarImage.sprite = avatarList[profileData.User.AvatarIndex];
            });
        }
        OnAvatarSelectionClick();
    }

    void Start()
    {
        Init();
    }

    void OnDestroy()
    {
        nameField.onEndEdit.RemoveListener(profileData.SetName);
        profileData.OnObtainedExperiencePoint -= OnObtainedExperiencePoint;
    }

    void OnObtainedExperiencePoint()
    {
        levelText.text = $"Level {profileData.User.Level}";
        experienceBar.value = profileData.User.ExperiencePoint / (float)profileData.GetRequirementExperiencePoint();
    }

    public void OnAvatarSelectionClick()
    {
        avatarSelectionRect.gameObject.SetActive(!avatarSelectionRect.gameObject.activeSelf);
    }

#if UNITY_EDITOR 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            profileData.ObtainExperiencePoint(2000);
        }
    }
#endif
}
