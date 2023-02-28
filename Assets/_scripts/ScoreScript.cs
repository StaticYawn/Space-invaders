using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] FloatVariable _gameScore;
    TextMeshProUGUI _text;

    private void Awake()
    {
        _gameScore.SetValue(0);
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetGameScore()
    {
        string tempScore = _gameScore.Value >= 99999 ? "99999" : _gameScore.Value.ToString().PadLeft(5, '0');
        _text.text = tempScore;
    }
}
