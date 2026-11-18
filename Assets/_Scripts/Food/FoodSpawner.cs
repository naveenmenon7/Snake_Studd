using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private GameObject _bonusFoodPrefab;

    private GameObject _currentFood;
    private Vector2Int _currentFoodPosition;
    private bool _isCurrentFoodBonus;

    public Vector2Int CurrentFoodPosition => _currentFoodPosition;
    public bool IsCurrentFoodBonus => _isCurrentFoodBonus;

    public event Action OnBoardFilled;

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

        if (_currentFood != null)
        {
            Destroy(_currentFood);
            _currentFood = null;
        }

        if (availablePositions.Count == 0)
        {
            _isCurrentFoodBonus = false;
            OnBoardFilled?.Invoke();
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
        _currentFoodPosition = availablePositions[randomIndex];

        _isCurrentFoodBonus = UnityEngine.Random.value < 0.2f;

        GameObject selectedPrefab = _isCurrentFoodBonus
            ? _bonusFoodPrefab
            : _foodPrefab;

        _currentFood = Instantiate(
            selectedPrefab,
            _boardManager.GetWorldPosition(_currentFoodPosition),
            Quaternion.identity,
            transform
        );

        if (_isCurrentFoodBonus)
        {
            _currentFood.GetComponent<BonusFood>().Initialize(this);
        }
    }

    public void HandleBonusFoodExpired(GameObject expiredFood)
    {
        if (_currentFood != expiredFood)
        {
            return;
        }

        _currentFood = null;
        SpawnFood();
    }
}