using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvent _updateScore;
    [SerializeField] GameEvent _updateHighscore;
    [SerializeField] GameEvent _updateLives;
    [SerializeField] GameEvent _spawnEnemy;

    [SerializeField] FloatVariable _gameScore;
    [SerializeField] FloatVariable _highscore;

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
        Pause();
    }
}
