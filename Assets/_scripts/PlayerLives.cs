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

    Animator _animator;

    SpriteRenderer _renderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (ResetHP)
        {
            SetHP();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("enemy_bullet"))
        {
            StartCoroutine(PlayerHitCrt());
        }
    }

    public void SetHP()
    {
        HP.SetValue(StartingHP.Value);
    }

    IEnumerator PlayerHitCrt()
    {
        Time.timeScale = 0;
        _animator.SetTrigger("ToBreakdown");
        HP.SetValue(HP.Value - 1);
        _hit.Raise();
        yield return new WaitForSecondsRealtime(1);
        _renderer.enabled = false;
        _animator.SetTrigger("ToEmpty");
        yield return new WaitForSecondsRealtime(1.5f);
        if (HP.Value == 0)
        {
            _death.Raise();
            yield break;
        }
        _renderer.enabled = true;
        transform.position = new Vector2(0, transform.position.y);
        Time.timeScale = 1;
    }
}
