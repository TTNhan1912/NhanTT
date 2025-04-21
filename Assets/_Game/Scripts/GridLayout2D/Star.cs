using DG.Tweening; // Nếu bạn sử dụng DOTween
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private SpriteRenderer candy;
    [SerializeField] private Sprite candyYellowSprite;
    [SerializeField] private Sprite candyGreySprite;

    public CandyType candyType; // Lưu loại kẹo

    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _swipeThreshold = 50f; // Ngưỡng để tính là vuốt
    private GridLayout2D _gridManager; // Quản lý lưới

    private void Start()
    {
        _gridManager = FindObjectOfType<GridLayout2D>(); // Lấy tham chiếu đến GridManager

        candyType = (CandyType)Random.Range(0, System.Enum.GetValues(typeof(CandyType)).Length);
        SetSpriteRenderer();
    }

    private void SetSpriteRenderer()
    {
        if (candyType == CandyType.Yellow)
        {
            candy.sprite = candyYellowSprite;
        }
        else
        {
            candy.sprite = candyGreySprite;
        }
    }

    void OnMouseDown()
    {
        _startPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        _endPosition = Input.mousePosition;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Vector2 direction = _endPosition - _startPosition;
        if (direction.magnitude < _swipeThreshold) return;

        Vector2Int swipeDirection = Vector2Int.zero;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            swipeDirection = direction.x > 0 ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            swipeDirection = direction.y > 0 ? Vector2Int.up : Vector2Int.down;
        }

        TrySwap(swipeDirection);
    }

    private void TrySwap(Vector2Int swipeDirection)
    {
        // Lấy vị trí viên kẹo kế bên
        Vector2 newPosition = (Vector2)transform.position + (Vector2)swipeDirection;

        RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.zero);
        if (hit.collider != null)
        {
            Star otherStar = hit.collider.GetComponent<Star>();
            if (otherStar != null)
            {
                if (otherStar.candyType == this.candyType)
                {
                    Debug.Log("Cùng loại! ");
                    CheckMatch();
                }
                else
                {
                    Debug.Log("Không cùng loại! Hoán đổi vị trí.");
                    SwapPosition(otherStar);
                }
            }
        }
    }

    private void SwapPosition(Star otherStar)
    {
        Vector3 tempPosition = transform.position;

        // Sử dụng DOTween để hoán đổi vị trí mượt mà
        transform.DOMove(otherStar.transform.position, 0.3f);
        otherStar.transform.DOMove(tempPosition, 0.3f);
    }

    private void CheckMatch()
    {
        // TODO: Kiểm tra hàng dọc/ngang có 3 viên kẹo cùng loại không
        //  Debug.Log("Kiểm tra hàng để thực hiện ăn...");
    }
}
