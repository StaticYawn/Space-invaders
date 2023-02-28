using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] FloatVariable _lives;

    public void UpdateLivesUi()
    {
        int index = (int)_lives.Value;

        Transform liveImage = transform.GetChild(index);
        liveImage.gameObject.SetActive(false);
    }

}
