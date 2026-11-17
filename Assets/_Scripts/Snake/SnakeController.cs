
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private GameObject _headPrefab;

    private GameObject _headObject;

    private Vector2Int _position;
    private Vector2Int _direction = Vector2Int.right;

    private float _moveInterval = 0.15f;
    private float _moveTimer;

    private void Start()
    {
        _position = new Vector2Int(10, 10);

        _headObject = Instantiate(
            _headPrefab,
            _boardManager.GetWorldPosition(_position),
            Quaternion.identity,
            transform
        );
    }

    private void Update()
    {
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
            && _direction != Vector2Int.down)
        {
            _direction = Vector2Int.up;
        }
        else if ((keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame)
            && _direction != Vector2Int.up)
        {
            _direction = Vector2Int.down;
        }
        else if ((keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
            && _direction != Vector2Int.right)
        {
            _direction = Vector2Int.left;
        }
        else if ((keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
            && _direction != Vector2Int.left)
        {
            _direction = Vector2Int.right;
        }
    }

    private void Move()
    {
        _position += _direction;

        Vector3 worldPosition = _boardManager.GetWorldPosition(_position);

        _headObject.transform.position = worldPosition;
    }
}
