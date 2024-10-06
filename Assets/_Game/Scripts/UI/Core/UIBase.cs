using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour
{
    [Header("BASE")]
    [SerializeField, ReadOnly] protected bool _isShowing;
    
    protected CanvasGroup _canvasGroup;
    protected UIManager _uiManager;
    protected EUILayer _uiLayer;
    public CanvasGroup CanvasGroup => _canvasGroup;
    public UIManager UIManager => _uiManager;

    public virtual void OnInit(UIManager uiManager)
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        _uiManager = uiManager;
        _canvasGroup.SetActive(false);
        _isShowing = false;
    }

    public virtual void OnShow()
    {
        _canvasGroup.SetActive(true);
        _isShowing = true;
    }


    public virtual void OnHide()
    {
        _canvasGroup.SetActive(false);
        _isShowing = false;
    }

    public virtual void OnRelease()
    {
    }

    public virtual bool OnBack()
    {
        return false;
    }

    public virtual string GetName()
    {
        return this.GetType().FullName;
    }

    public virtual EUILayer GetLayer()
    {
        return _uiLayer;
    }

}