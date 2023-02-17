using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] BoolVariable shot;
    [SerializeField] private FloatReference ShotSpeed;

    private enum Direction { up, down, left, right }
    [SerializeField] private Direction direction;

    private Vector2 shotDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = shotDirection * ShotSpeed.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == gameObject) return;
        shot.SetFalse();
        Destroy(gameObject);
    }
}
