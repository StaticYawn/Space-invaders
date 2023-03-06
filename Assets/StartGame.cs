using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    Button _button;
    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(LoadMainGame);
    }

    void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
