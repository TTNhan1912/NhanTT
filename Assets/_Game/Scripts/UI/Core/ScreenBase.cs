using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class ScreenBase : UIBase
{
    public override EUILayer GetLayer()
    {
        return EUILayer.Screen;
    }
}
