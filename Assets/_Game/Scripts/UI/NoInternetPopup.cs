using System;
using UnityEngine;
using UnityEngine.UI;

public class NoInternetPopup : PopupBase
{
    [SerializeField] private Button _okButton;

    public override EUILayer GetLayer()
    {
        return EUILayer.AlwaysOnTop;
    }


    private void Awake()
    {
        _okButton.onClick.AddListener(OnBtnOkClicked);
    }

    private void Update()
    {
        if (_isShowing)
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                OnHide();
            }
        }

        if (Application.internetReachability == NetworkReachability.NotReachable && !_isShowing)
        {
            OnShow();
        }
    }

    private void OnBtnOkClicked()
    {
        try
        {
#if UNITY_ANDROID
            using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.WIFI_SETTINGS"))
                {
                    currentActivityObject.Call("startActivity", intentObject);
                }
            }
#elif UNITY_IOS
        OnHide();
#endif
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}