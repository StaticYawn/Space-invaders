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

    private void Awake()
    {
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
        if (collision.collider == gameObject) return;
        _shot.SetFalse();
        Destroy(gameObject);
    }
}
