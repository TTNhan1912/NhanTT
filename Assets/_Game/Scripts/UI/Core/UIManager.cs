using Framework;
using Redcode.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EUILayer
{
    Screen,
    Popup,
    AlwaysOnTop
}

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] protected ScreenBase[] _arrScreens = Array.Empty<ScreenBase>();
    [SerializeField] protected PopupBase[] _arrPopups = Array.Empty<PopupBase>();
    [SerializeField] protected RectTransform[] _layerRectTransforms = Array.Empty<RectTransform>();

    protected Dictionary<string, ScreenBase> _dicScreens = new Dictionary<string, ScreenBase>();
    protected Dictionary<string, PopupBase> _dicPopups = new Dictionary<string, PopupBase>();
    private Dictionary<EUILayer, RectTransform> _layerRectTfDictCached = new Dictionary<EUILayer, RectTransform>();

    private readonly string _uiRootPath = "Prefabs/";


    public void OnInit()
    {
        var layerNames = Enum.GetNames(typeof(EUILayer));
        if (_layerRectTransforms.Length == layerNames.Length)
        {
            for (int i = 0; i < layerNames.Length; i++)
            {
                var uiLayer = (EUILayer)Enum.Parse(typeof(EUILayer), layerNames[i]);
                _layerRectTfDictCached.Add(uiLayer, _layerRectTransforms[i]);
            }
        }
        

        foreach (var screen in _arrScreens)
        {
            var screenName = screen.GetName();

            if (!_dicScreens.TryAdd(screenName, screen))
            {
                Debug.LogError("Invalid screen name " + screenName + " of gameObject " + screen.gameObject.name);
            }
        }

        foreach (var screen in _arrScreens)
        {
            screen.OnInit(this);
        }

        foreach (var popup in _arrPopups)
        {
            var popupName = popup.GetName();
            _dicPopups.TryAdd(popupName, popup);
        }

        foreach (var popup in _arrPopups)
        {
            popup.OnInit(this);
        }
    }


    public void OnRelease()
    {
        foreach (var screen in _arrScreens)
        {
            screen.OnRelease();
        }
    }


    public T ShowScreen<T>() where T : ScreenBase
    {
        var screen = GetScreen<T>();
        if( screen == null)
        {
            Debug.LogError("Invalid screen " + typeof(T).FullName);
            return null;
        }
        screen.OnShow();
        return screen;
    }


    public T HideScreen<T>() where T : ScreenBase
    {
        var screen = GetScreen<T>();
        if (screen == null)
        {
            Debug.LogError("Invalid screen " + typeof(T).FullName);
            return null;
        }
        screen.OnHide();
        return screen;
    }


    public T GetScreen<T>() where T : ScreenBase
    {
        var screenName = typeof(T).FullName;

        if (_dicScreens.TryGetValue(screenName, out var screen))
        {
            return screen as T;
        }

        try
        {
            var prefab = Resources.Load<GameObject>(_uiRootPath + $"{screenName}").GetComponent<ScreenBase>();
            if (prefab != null)
            {
                var cloneScreen = Instantiate(prefab, Vector3.zero, Quaternion.identity,_layerRectTfDictCached.TryGetValue(prefab.GetLayer() , out var parentRect) ? parentRect : this.transform );
                cloneScreen.OnInit(this);
                cloneScreen.SetFullScreen();

                if (!_dicScreens.TryAdd(screenName, cloneScreen))
                {
                    Destroy(cloneScreen.gameObject);
                    Debug.LogError("Invalid screen name " + screenName + " of gameObject " + cloneScreen.gameObject.name);
                }

                return cloneScreen as T;
            }
            else
            {
                Debug.LogError("Prefab can not find in Resources/Prefab folder !");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error : " + ex.Message);
        }

        return null;
    }


    public T ShowPopup<T>() where T : PopupBase
    {
        var popup = GetPopup<T>();
        if (popup == null)
        {
            Debug.LogError("Invalid popup " + typeof(T).FullName);
            return null;
        }
        popup.OnShow();
        return popup;
    }


    public T HidePopup<T>() where T : PopupBase
    {
        var popup = GetPopup<T>();
        if (popup == null)
        {
            Debug.LogError("Invalid popup " + typeof(T).FullName);
            return null;
        }
        popup.OnHide();
        return popup;

    }


    public T GetPopup<T>() where T : PopupBase
    {
        var popupName = typeof(T).FullName;

        if (_dicPopups.TryGetValue(popupName, out var popup))
        {
            return popup as T;
        }
        try
        {
            var prefab = Resources.Load<GameObject>(_uiRootPath + $"{popupName}").GetComponent<PopupBase>();
            if (prefab != null)
            {
                var clonePopup = Instantiate(prefab, Vector3.zero, Quaternion.identity, _layerRectTfDictCached.TryGetValue(prefab.GetLayer(), out var parentRect) ? parentRect : transform);
                clonePopup.OnInit(this);
                clonePopup.SetFullScreen();

                if (!_dicPopups.TryAdd(popupName, clonePopup))
                {
                    Destroy(clonePopup.gameObject);
                    Debug.LogError("Invalid popup name " + popupName + " of gameObject " + clonePopup.gameObject.name);
                }

                return clonePopup as T;
            }
            else
            {
                Debug.LogError("Prefab can not find in Resources/Prefab folder !");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error get popup : " + ex.Message);
        }

        return null;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        _arrScreens = GetComponentsInChildrenRecursive<ScreenBase>(transform).ToArray();
        _arrPopups = GetComponentsInChildrenRecursive<PopupBase>(transform).ToArray();
    }

    public List<T> GetComponentsInChildrenRecursive<T>(Transform parent)
    {
       var components = new List<T>();

        var currentComponents = parent.GetComponents<T>();
        components.AddRange(currentComponents);

        foreach (Transform child in parent)
        {
            components.AddRange(GetComponentsInChildrenRecursive<T>(child));
        }

        return components;
    }


#endif


}
