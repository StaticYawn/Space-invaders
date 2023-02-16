using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public FloatVariable HP;
    public bool ResetHP;
    public FloatReference StartingHP;
    // Start is called before the first frame update
    void Start()
    {
        if (ResetHP)
        {
            HP.SetValue(StartingHP.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
