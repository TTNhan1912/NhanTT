using UnityEngine;

public abstract class Keo : MonoBehaviour
{
    [SerializeField] private RectTransform _rect1;
    [SerializeField] private RectTransform[] _rect2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("interac"))
        {
            OnInterac();
        }
    }

    protected abstract void OnInterac();

}
