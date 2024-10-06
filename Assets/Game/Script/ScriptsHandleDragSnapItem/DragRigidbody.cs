using UnityEngine;
using UnityEngine.Events;

public class DragRigidbody : MonoBehaviour
{
    public bool AutoConnectDragAnimation;
    public bool RigidbodySimulated;

    bool isDragging/* , isDisable; */;
    public UnityEvent OnBeginDrag, OnEndDrag;

    Rigidbody2D rigid;
    DragAnimation dragAnimation;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (AutoConnectDragAnimation)
        {
            dragAnimation = GetComponent<DragAnimation>();
        }
        // GameManager.WinAction += OnWin;
    }

    private void OnDestroy()
    {
        // GameManager.WinAction -= OnWin;
    }

    void OnWin() => Destroy(this);

    private void OnMouseDown()
    {
        if (Draggable.IsPause()) return;

        offset = gameObject.transform.position - GetMouseWorldPos();
        dragAnimation?.OnBeginDrag();
        OnBeginDrag?.Invoke();
        SetRigidbodySimulated(false);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (Draggable.IsPause()) return;

        dragAnimation?.OnEndDrag();
        OnEndDrag?.Invoke();
        rigid.velocity = Vector2.zero;
        SetRigidbodySimulated(true);
        isDragging = false;
    }

    void SetRigidbodySimulated(bool value)
    {
        if (!RigidbodySimulated)
        {
            rigid.simulated = value;
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            if (!RigidbodySimulated)
            {
                transform.position = GetMouseWorldPos() + offset;
            }
            else
            {
                rigid.MovePosition(GetMouseWorldPos() + offset);
            }
        }
    }

    public void Disable()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }


    Vector3 mousePos, offset;
    Vector3 GetMouseWorldPos()
    {
        mousePos = UnityEngine.Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
