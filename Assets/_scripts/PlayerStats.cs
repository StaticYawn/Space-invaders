using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] FloatVariable _lives;
    [SerializeField] GameObject _liveImage;

    private void Start()
    {
        SetLivesUI();
    }

    public void SetLivesUI()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < _lives.Value; i++)
        {
            Instantiate(_liveImage, transform);
        }
    }
}
