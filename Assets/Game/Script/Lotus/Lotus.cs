using UnityEngine;
using UnityEngine.Events;

public class Lotus : MonoBehaviour
{
    public UnityEvent OnPick, OnDrag, OnEndDrag;

    public UnityEvent<Transform> OnPickTrans;

    private void OnMouseDown()
    {
        OnPick?.Invoke();
        OnPickTrans?.Invoke(transform);
    }

    private void OnMouseUp()
    {
        OnEndDrag?.Invoke();
    }

    private void OnMouseDrag()
    {
        OnDrag?.Invoke();

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 10f;

        transform.position = pos;

    }


}
