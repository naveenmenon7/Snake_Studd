using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FoodSpawner _foodSpawner;

    public enum GameState
    {
        Ready,
        Playing,
        GameOver,
        Won
    }

    public GameState CurrentState { get; private set; } = GameState.Ready;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnGameOver;
    public event Action<int> OnGameWon;

    private int _score;

    public int Score => _score;

    private void OnEnable()
    {
        _foodSpawner.OnBoardFilled += HandleBoardFilled;
    }

    private void OnDisable()
    {
        _foodSpawner.OnBoardFilled -= HandleBoardFilled;
    }

    private void HandleBoardFilled()
    {
        CompleteGame();
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
    }

    public void AddScore(int points)
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        _score += points;
        OnScoreChanged?.Invoke(_score);
    }

    public void EndGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.GameOver;
        OnGameOver?.Invoke(_score);
    }

    public void CompleteGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.Won;
        OnGameWon?.Invoke(_score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}