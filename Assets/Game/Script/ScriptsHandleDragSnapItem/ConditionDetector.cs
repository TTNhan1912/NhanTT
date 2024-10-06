using RDG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConditionDetector : MonoBehaviour
{
    public bool CompleteLevel = true;
    public bool VibrateWithEachStep = true;
    public int StepCount;
    public UnityEvent OnLevelComplete, OnStep;

    public static Action CountAction;

    private void Awake()
    {
        CountAction += OnCount;
    }

    private void OnDestroy()
    {
        CountAction -= OnCount;
    }

    private void OnCount()
    {
        StepCount--;
        OnStep?.Invoke();

        if (StepCount == 0)
        {
            OnLevelComplete?.Invoke();
            if (CompleteLevel)
            {
                //   GameManager.Get.CompleteCurrentLevel();
            }
        }
        else
        {
            if (VibrateWithEachStep)
            {
                Vibration.Vibrate(60);
            }
        }
    }
}

public class InputCustom
{
    public static bool Interactable()
    {
        PointerEventData eventDataCurrentPosition = new(EventSystem.current)
        {
            position = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y)
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.FindAll(x => x.gameObject.layer == LayerMask.NameToLayer("UI")).Count == 0;
    }
}
