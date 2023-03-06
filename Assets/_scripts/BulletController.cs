using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] BoolVariable _shot;
    [SerializeField] private FloatReference _shotSpeed;

    private enum Direction { up, down, left, right }
    [SerializeField] private Direction direction;

    private Vector2 shotDirection;

    [SerializeField] Sprite _explosionSprite, _crash;

    SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        shotDirection =
            direction == Direction.up ? Vector2.up :
            direction == Direction.down ? Vector2.down :
            direction == Direction.left ? Vector2.left :
            Vector2.right;
    }


    void Update()
    {
        _rb.velocity = shotDirection * _shotSpeed.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _rb.simulated = false;
        if (collision.collider == gameObject) return;
        _shot.SetFalse();

        StartCoroutine(BulletExplosionCrt(collision.collider));

    }

    IEnumerator BulletExplosionCrt(Collider2D collider)
    {
        if (gameObject.CompareTag("enemy_bullet"))
        {
            if (collider.CompareTag("player_bullet"))
            {
                _renderer.sprite = _explosionSprite;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                _renderer.sprite = _crash;
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            if (!collider.CompareTag("Enemy") && !collider.CompareTag("enemy_bullet"))
            {
                _renderer.sprite = _crash;
                yield return new WaitForSeconds(0.2f);
            }
        }

        Destroy(gameObject);
    }
}
