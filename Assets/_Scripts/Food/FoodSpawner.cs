using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private GameObject _foodPrefab;

    private GameObject _currentFood;
    private Vector2Int _currentFoodPosition;

    public Vector2Int CurrentFoodPosition => _currentFoodPosition;

    private void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        for (int x = 0; x < _boardManager.Width; x++)
        {
            for (int y = 0; y < _boardManager.Height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (!_snakeController.IsPositionOccupied(position))
                {
                    availablePositions.Add(position);
                }
            }
        }

        if (availablePositions.Count == 0)
        {
            Debug.LogWarning("No available positions left for food.");
            return;
        }

        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector2Int randomPosition = availablePositions[randomIndex];

        _currentFoodPosition = randomPosition;

        Vector3 worldPosition = _boardManager.GetWorldPosition(randomPosition);

        if (_currentFood != null)
        {
            Destroy(_currentFood);
        }

        _currentFood = Instantiate(
            _foodPrefab,
            worldPosition,
            Quaternion.identity,
            transform
        );
    }
}