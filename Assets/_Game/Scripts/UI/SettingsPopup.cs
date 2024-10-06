using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsPopup : PopupBase
{

    [SerializeField] private bool _isOnlyOnLocal;

    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _vibrateButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Image _musicIconImage;
    [SerializeField] private Image _soundIconImage;
    [SerializeField] private Image _vibrateIconImage;

    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private Sprite _vibrateOnSprite;
    [SerializeField] private Sprite _vibrateOffSprite;

    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _moveDuration;

    public Action HideSettingCallback;

    #region PROPERTIES

    private bool _isSoundOn
    {
        get
        {
            if (_isOnlyOnLocal)
                return PlayerPrefs.GetInt("IsSoundOn", 1) == 1;
            else
                return DataManager.Instance.GetData<UserData>().HasSound();
        }
        set
        {
            _soundIconImage.sprite = value ? _soundOnSprite : _soundOffSprite;
            if (_isOnlyOnLocal)
            {
                PlayerPrefs.SetInt("IsSoundOn", value ? 1 : 0);
            }
            else
            {
                DataManager.Instance.GetData<UserData>().SetSound(value);
            }
        }
    }

    private bool _isMusicOn
    {
        get
        {
            if (_isOnlyOnLocal)
                return PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
            else
                return DataManager.Instance.GetData<UserData>().HasMusic();
        }
        set
        {
            _musicIconImage.sprite = value ? _musicOnSprite : _musicOffSprite;
            if (_isOnlyOnLocal)
            {
                PlayerPrefs.SetInt("IsMusicOn", value ? 1 : 0);
            }
            else
            {
                DataManager.Instance.GetData<UserData>().SetMusic(value);
            }
        }
    }

    private bool _isVibrateOn
    {
        get
        {
            if (_isOnlyOnLocal)
                return PlayerPrefs.GetInt("IsVibrateOn", 1) == 1;
            else
                return DataManager.Instance.GetData<UserData>().HasVibration();
        }
        set
        {
            _vibrateIconImage.sprite = value ? _vibrateOnSprite : _vibrateOffSprite;
            if (_isOnlyOnLocal)
            {
                PlayerPrefs.SetInt("IsVibrateOn", value ? 1 : 0);
            }
            else
            {
                DataManager.Instance.GetData<UserData>().SetVibration(value);
            }
        }
    }

    #endregion


    public override void OnInit(UIManager uiManager)
    {
        base.OnInit(uiManager);

        _musicButton.onClick.AddListener(() =>
        {

            _isMusicOn = !_isMusicOn;
        });

        _soundButton.onClick.AddListener(() =>
        {
          
            _isSoundOn = !_isSoundOn;
        });

        _vibrateButton.onClick.AddListener(() =>
        {

            _isVibrateOn = !_isVibrateOn;
        });

        _closeButton.onClick.AddListener(() =>
        {
            OnHide();
            HideSettingCallback?.Invoke();
            HideSettingCallback = null;
           
        });

    }


    public void SubscribeCallback(Action callback)
    {
        HideSettingCallback += callback;
    }


    private void Refresh()
    {
        _soundIconImage.sprite = _isSoundOn ? _soundOnSprite : _soundOffSprite;
        _musicIconImage.sprite = _isMusicOn ? _musicOnSprite : _musicOffSprite;
        _vibrateIconImage.sprite = _isVibrateOn ? _vibrateOnSprite : _vibrateOffSprite;
    }


    public override void OnShow()
    {
        Refresh();
        _panel.transform.DOKill();
        _panel.transform.localScale = Vector3.zero;
        base.OnShow();
        _panel.transform.DOScale(Vector3.one, _moveDuration).SetEase(Ease.OutBack);
    }

}
