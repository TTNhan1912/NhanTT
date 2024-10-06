using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    [SerializeField] bool autoConnectDragAnimation;
    protected bool isDragging;
    public UnityEvent OnBeginDrag, OnEndDrag;

    Collider2D collider2d;
    DragAnimation dragAnimation;
    public void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        if (autoConnectDragAnimation)
        {
            dragAnimation = GetComponent<DragAnimation>();
        }
    }

    private void Start() { }

    private void OnMouseDown()
    {
        if (IsPause()) return;

        offset = gameObject.transform.position - GetMouseWorldPos();
        dragAnimation?.OnBeginDrag();
        OnBeginDrag?.Invoke();
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (IsPause()) return;

        dragAnimation?.OnEndDrag();
        OnEndDrag?.Invoke();
        isDragging = false;
    }

    protected virtual void Update()
    {
        if (IsPause()) return;
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    protected Vector3 mousePos, offset;
    protected virtual Vector3 GetMouseWorldPos()
    {
        mousePos = UnityEngine.Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void Disable()
    {
        collider2d.enabled = false;
        enabled = false;
    }

    private void OnDisable()
    {
        isDragging = false;
    }

    public void SetInteractable(bool value)
    {
        collider2d.enabled = value;
        isDragging = false;
    }

    public static bool IsPause()
    {
        //  return PopupSettings.Showing;

        return false;
        // PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        // {
        //     position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        // };
        // List<RaycastResult> results = new();
        // EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        // //return results.Count > 0;
        // return results.FindAll(x => x.gameObject.layer == LayerMask.NameToLayer("UI")).Count > 0;
    }
}
