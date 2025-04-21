using UnityEngine;

public class GridLayout2D : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform parent;
    public int gridWidth = 8, gridHeight = 8;
    public float cellSize = 1.1f; // Kích thước ô (gồm padding)
    public GameObject[,] gridObjects; // Lưu trữ các object trong lưới

    private void Awake()
    {
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        gridObjects = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0); // Cộng thêm padding
                GameObject newObj = Instantiate(objectPrefab, position, Quaternion.identity, parent);
                newObj.transform.localPosition = position;
                gridObjects[x, y] = newObj;
            }
        }
    }

    public void TrySwap(GameObject obj, Vector2Int direction)
    {
        Vector2Int objPos = GetGridPosition(obj);
        Vector2Int targetPos = objPos + direction;

        if (!IsValidPosition(targetPos)) return;

        GameObject targetObj = gridObjects[targetPos.x, targetPos.y];

        // Hoán đổi vị trí
        Swap(obj, targetObj);

        // Kiểm tra có tạo thành bộ 3 không
        if (!CheckMatch(obj) && !CheckMatch(targetObj))
        {
            // Nếu không tạo match, hoán đổi lại
            Swap(obj, targetObj);
        }
        else
        {
            Debug.Log("Có thể ăn!");
            DestroyMatches();
        }
    }

    private Vector2Int GetGridPosition(GameObject obj)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (gridObjects[x, y] == obj)
                    return new Vector2Int(x, y);
            }
        }
        return Vector2Int.zero;
    }

    private bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridWidth && pos.y >= 0 && pos.y < gridHeight;
    }

    private void Swap(GameObject a, GameObject b)
    {
        Vector2Int posA = GetGridPosition(a);
        Vector2Int posB = GetGridPosition(b);

        gridObjects[posA.x, posA.y] = b;
        gridObjects[posB.x, posB.y] = a;

        Vector3 tempPos = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = tempPos;
    }

    private bool CheckMatch(GameObject obj)
    {
        Vector2Int pos = GetGridPosition(obj);
        string objType = obj.tag; // Hoặc dùng script để lấy loại

        // Kiểm tra theo hàng ngang
        if (CountMatches(pos, Vector2Int.right) + CountMatches(pos, Vector2Int.left) >= 2)
            return true;

        // Kiểm tra theo hàng dọc
        if (CountMatches(pos, Vector2Int.up) + CountMatches(pos, Vector2Int.down) >= 2)
            return true;

        return false;
    }

    private int CountMatches(Vector2Int startPos, Vector2Int direction)
    {
        int count = 0;
        Vector2Int pos = startPos + direction;

        while (IsValidPosition(pos) && gridObjects[pos.x, pos.y]?.tag == gridObjects[startPos.x, startPos.y]?.tag)
        {
            count++;
            pos += direction;
        }

        return count;
    }

    private void DestroyMatches()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (CheckMatch(gridObjects[x, y]))
                {
                    Destroy(gridObjects[x, y]);
                    gridObjects[x, y] = null;
                }
            }
        }
    }
}
public enum CandyType
{
    Grey,
    Yellow,
    Red,
    Blue,
    Green
}