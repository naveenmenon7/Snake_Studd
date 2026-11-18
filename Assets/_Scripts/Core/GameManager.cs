using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private AudioManager _audioManager;

    public enum GameState
    {
        Ready,
        Playing,
        Paused,
        GameOver,
        Won
    }

    public GameState CurrentState { get; private set; } = GameState.Ready;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnGameOver;
    public event Action<int> OnGameWon;
    public event Action<bool> OnPauseStateChanged;

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

    private void Update()
    {
        if (Keyboard.current == null ||
            !Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (CurrentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (CurrentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    private void HandleBoardFilled()
    {
        CompleteGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
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

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.Paused;
        Time.timeScale = 0f;

        OnPauseStateChanged?.Invoke(true);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
        {
            return;
        }

        Time.timeScale = 1f;
        CurrentState = GameState.Playing;

        OnPauseStateChanged?.Invoke(false);
    }

    public void EndGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.GameOver;

        _audioManager.PlaySnakeDeath();

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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}