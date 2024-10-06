using DG.Tweening;
using UnityEngine;

public class DragAnimation : MonoBehaviour
{
    [SerializeField] bool sortingOrder, rotate, scale;
    Tween tweenRotate, tweenScale;

    SpriteRenderer spriteRenderer;
    int defaultSortingOrder;
    Vector2 defaultScale = Vector2.one;

    private void Awake()
    {
        if (sortingOrder)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSortingOrder = spriteRenderer.sortingOrder;
            defaultScale = transform.localScale;
        }
    }

    public void OnBeginDrag()
    {
        if (sortingOrder) spriteRenderer.sortingOrder += 10;

        if (rotate) tweenRotate = transform.DORotate(Vector3.zero, 0.2f);

        if (scale) tweenScale = transform.DOScale(defaultScale * 1.1f, 0.2f);
    }

    public void OnEndDrag()
    {
        if (sortingOrder) spriteRenderer.sortingOrder = defaultSortingOrder;

        if (rotate) tweenRotate = transform.DORotate(new Vector3(0, 0, Random.Range(-10, 10)), 0.2f);

        if (scale) tweenScale = transform.DOScale(defaultScale, 0.2f);
    }
}
