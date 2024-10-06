using System;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItem : MonoBehaviour
{
    Image image;
    Button button;
    int avatarId;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void Init(Sprite sprite, int id, Action<int> onClick)
    {
        image.sprite = sprite;
        avatarId = id;

        button.onClick.AddListener(() =>
        {
            onClick?.Invoke(avatarId);
        });
    }
}
