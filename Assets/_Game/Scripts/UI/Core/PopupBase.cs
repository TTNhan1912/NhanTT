using UnityEngine;

public class PopupBase : UIBase
{
    public override EUILayer GetLayer()
    {
        return EUILayer.Popup;
    }
}
