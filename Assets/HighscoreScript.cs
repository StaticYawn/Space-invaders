using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreScript : MonoBehaviour
{
    [SerializeField] FloatVariable _gameScore;
    [SerializeField] FloatVariable _highscore;
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetHighScore();
    }

    public void SetHighScore()
    {
        if (_gameScore.Value > _highscore.Value) _highscore.SetValue(_gameScore.Value);
        string tempScore = _highscore.Value >= 99999 ? "99999" : _highscore.Value.ToString().PadLeft(5, '0');
        _text.text = tempScore;
    }
}
