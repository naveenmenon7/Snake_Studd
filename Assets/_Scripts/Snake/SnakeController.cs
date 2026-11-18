using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _headPrefab;
    [SerializeField] private GameObject _bodyPrefab;

    private List<Vector2Int> _snakePositions = new List<Vector2Int>();
    private List<GameObject> _bodySegments = new List<GameObject>();

    private GameObject _headObject;

    private Vector2Int _direction = Vector2Int.right;
    private Vector2Int _nextDirection = Vector2Int.right;

    private float _moveInterval = 0.15f;
    private float _moveTimer;

    private void Start()
    {
        Vector2Int startingPosition = new Vector2Int(10, 10);

        _snakePositions.Add(startingPosition);

        _headObject = Instantiate(
            _headPrefab,
            _boardManager.GetWorldPosition(startingPosition),
            Quaternion.identity,
            transform
        );

        for (int i = 1; i <= 2; i++)
        {
            Vector2Int bodyPosition = startingPosition - Vector2Int.right * i;

            _snakePositions.Add(bodyPosition);

            GameObject bodySegment = Instantiate(
                _bodyPrefab,
                _boardManager.GetWorldPosition(bodyPosition),
                Quaternion.identity,
                transform
            );

            _bodySegments.Add(bodySegment);
        }

        _foodSpawner.SpawnFood();
        _gameManager.StartGame();
    }

    private void Update()
    {
        if (_gameManager.CurrentState != GameManager.GameState.Playing)
        {
            return;
        }

        HandleInput();

        _moveTimer += Time.deltaTime;

        if (_moveTimer >= _moveInterval)
        {
            Move();
            _moveTimer -= _moveInterval;
        }
    }

    private void HandleInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return;
        }

        if ((keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
            && _nextDirection != Vector2Int.down
            && _direction != Vector2Int.down)
        {
            _nextDirection = Vector2Int.up;
        }
        else if ((keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame)
            && _nextDirection != Vector2Int.up
            && _direction != Vector2Int.up)
        {
            _nextDirection = Vector2Int.down;
        }
        else if ((keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
            && _nextDirection != Vector2Int.right
            && _direction != Vector2Int.right)
        {
            _nextDirection = Vector2Int.left;
        }
        else if ((keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
            && _nextDirection != Vector2Int.left
            && _direction != Vector2Int.left)
        {
            _nextDirection = Vector2Int.right;
        }
    }

    private void Move()
    {
        _direction = _nextDirection;

        Vector2Int newHeadPosition = _snakePositions[0] + _direction;

        if (!_boardManager.IsInsideBoard(newHeadPosition))
        {
            Debug.Log("Game Over - Wall Collision");
            _gameManager.EndGame();
            return;
        }

        bool collectedFood = newHeadPosition == _foodSpawner.CurrentFoodPosition;
        bool collectedBonus = collectedFood && _foodSpawner.IsCurrentFoodBonus;

        int collisionCheckCount = collectedFood
            ? _snakePositions.Count
            : _snakePositions.Count - 1;

        for (int i = 0; i < collisionCheckCount; i++)
        {
            if (_snakePositions[i] == newHeadPosition)
            {
                Debug.Log("Game Over - Self Collision");
                _gameManager.EndGame();
                return;
            }
        }

        _snakePositions.Insert(0, newHeadPosition);

        if (collectedFood)
        {
            int growthAmount = collectedBonus ? 2 : 1;
            int scoreAmount = collectedBonus ? 30 : 10;

            Grow(growthAmount);

            _gameManager.AddScore(scoreAmount);

            _audioManager.PlayFoodCollect(collectedBonus);

            _foodSpawner.SpawnFood();
        }
        else
        {
            _snakePositions.RemoveAt(_snakePositions.Count - 1);
        }

        _headObject.transform.position =
            _boardManager.GetWorldPosition(newHeadPosition);

        UpdateBodyVisuals();
    }

    private void Grow(int amount)
    {
        Vector2Int tailPosition = _snakePositions[_snakePositions.Count - 1];

        for (int i = 0; i < amount; i++)
        {
            if (i > 0)
            {
                _snakePositions.Add(tailPosition);
            }

            GameObject bodySegment = Instantiate(
                _bodyPrefab,
                _boardManager.GetWorldPosition(tailPosition),
                Quaternion.identity,
                transform
            );

            _bodySegments.Add(bodySegment);
        }
    }

    private void UpdateBodyVisuals()
    {
        for (int i = 0; i < _bodySegments.Count; i++)
        {
            Vector2Int bodyPosition = _snakePositions[i + 1];

            _bodySegments[i].transform.position =
                _boardManager.GetWorldPosition(bodyPosition);
        }
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return _snakePositions.Contains(position);
    }
}