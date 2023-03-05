using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvent _updateScore;
    [SerializeField] GameEvent _updateHighscore;
    [SerializeField] GameEvent _updateLives;
    [SerializeField] GameEvent _spawnEnemy;
    [SerializeField] GameEvent _resetGame;

    [SerializeField] FloatVariable _gameScore;
    [SerializeField] FloatVariable _highscore;

    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _pauseScreen;

    [SerializeField] Button _quitButton1, _quitButton2, _resumeButton, _restartButton;

    private void Start()
    {
        _quitButton1.onClick.AddListener(QuitGame);
        _quitButton2.onClick.AddListener(QuitGame);

        _resumeButton.onClick.AddListener(ResumeGame);
        _restartButton.onClick.AddListener(RestartGame);
    }

    public void UpdateScoreUI()
    {
        _updateScore.Raise();
    }

    public void UpdateHighscoreUI()
    {
        _updateHighscore.Raise();
    }

    public void UpdateHealthUI()
    {
        _updateLives.Raise();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ResetEnemies()
    {
        _spawnEnemy.Raise();
    }

    public void OnPlayerDeath()
    {
        UpdateHighscoreUI();
        ShowGameOverMenu();
        Pause();
    }

    public void ShowGameOverMenu()
    {
        _gameOverScreen.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        if (_pauseScreen.activeInHierarchy) return;
        Pause();
        _pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Resume();
        _pauseScreen.SetActive(false);
    }

    public void RestartGame()
    {
        Resume();
        _gameOverScreen.SetActive(false);
        _resetGame.Raise();
        ResetEnemies();
        _gameScore.SetValue(0);
        UpdateHealthUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
