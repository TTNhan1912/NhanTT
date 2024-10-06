using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDragHandler : MonoBehaviour
{
    [SerializeField] private Transform correctPoint;
    [SerializeField] private float distance;
    [SerializeField] bool hasShadow;
    public UnityEvent OnDone;

    SpriteRenderer image, shadow;
    DragRigidbody draggable;
    static int Layer;

    private void Awake()
    {
        draggable = GetComponent<DragRigidbody>();
        image = GetComponent<SpriteRenderer>();
        if (hasShadow) shadow = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (image != null)
        {
            Layer = 0;
            image.sortingOrder = 0;
        }

        draggable.OnBeginDrag.AddListener(OnBeginDrag);
        draggable.OnEndDrag.AddListener(OnEndDrag);
    }


    public void OnBeginDrag()
    {
        if (image != null)
        {
            Layer += 2;
            image.sortingOrder = Layer;
            if (hasShadow) shadow.sortingOrder = Layer - 1;
        }
    }

    public void OnEndDrag()
    {
        if (Vector2.Distance(transform.position, correctPoint.position) <= distance)
        {
            draggable.Disable();

            if (hasShadow) shadow.gameObject.SetActive(false);
            transform.DORotate(Vector2.zero, 0.2f).SetEase(Ease.Linear);
            transform.DOMove(correctPoint.position, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                image.sortingOrder = -5;
                ConditionDetector.CountAction?.Invoke();
            });

            OnDone?.Invoke();
        }
    }
}
