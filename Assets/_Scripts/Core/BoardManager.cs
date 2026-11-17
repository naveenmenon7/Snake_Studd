using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int _width = 20;
    [SerializeField] private int _height = 20;

    public int Width => _width;
    public int Height => _height;

    public bool IsInsideBoard(Vector2Int position)
    {
        return position.x >= 0 &&
               position.x < _width &&
               position.y >= 0 &&
               position.y < _height;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float worldX = gridPosition.x - (_width / 2f) + 0.5f;
        float worldY = gridPosition.y - (_height / 2f) + 0.5f;

        return new Vector3(worldX, worldY, 0f);
    }
}