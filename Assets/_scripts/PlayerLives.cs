using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public FloatVariable HP;
    public bool ResetHP;
    public FloatReference StartingHP;

    [SerializeField] GameEvent _hit;
    [SerializeField] GameEvent _death;

    void Start()
    {
        if (ResetHP)
        {
            HP.SetValue(StartingHP.Value);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("enemy_bullet"))
        {
            HP.SetValue(HP.Value - 1);
            _hit.Raise();
            if(HP.Value == 0) _death.Raise();
        }
    }
}
