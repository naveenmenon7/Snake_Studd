using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverTitle;

    private void OnEnable()
    {
        _gameManager.OnScoreChanged += UpdateScore;
        _gameManager.OnGameOver += ShowGameOver;
        _gameManager.OnGameWon += ShowVictory;
    }

    private void OnDisable()
    {
        _gameManager.OnScoreChanged -= UpdateScore;
        _gameManager.OnGameOver -= ShowGameOver;
        _gameManager.OnGameWon -= ShowVictory;
    }

    private void Start()
    {
        UpdateScore(_gameManager.Score);
        _gameOverPanel.SetActive(false);
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }

    private void ShowGameOver(int finalScore)
    {
        _gameOverTitle.text = "GAME OVER";
        _gameOverTitle.color = new Color(1f, 0.32f, 0.39f);

        ShowEndScreen(finalScore);
    }

    private void ShowVictory(int finalScore)
    {
        _gameOverTitle.text = "YOU WIN!";
        _gameOverTitle.color = new Color(0.22f, 0.91f, 0.47f);

        ShowEndScreen(finalScore);
    }

    private void ShowEndScreen(int finalScore)
    {
        _finalScoreText.text = "FINAL SCORE: " + finalScore;
        _gameOverPanel.SetActive(true);
    }
}
