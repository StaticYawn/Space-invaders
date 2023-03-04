using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] FloatVariable _score;
    [SerializeField] bool _resetScore;
    TextMeshProUGUI _text;

    private void Awake()
    {
        if(_resetScore) _score.SetValue(0);
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetGameScore()
    {
        string tempScore = _score.Value >= 99999 ? "99999" : _score.Value.ToString().PadLeft(5, '0');
        _text.text = tempScore;
    }
}
