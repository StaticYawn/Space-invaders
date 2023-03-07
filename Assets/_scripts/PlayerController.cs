using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatReference _moveSpeed;
    [SerializeField] BoolVariable _shot;
    private bool _movingLeft = false;
    private bool _movingRight = false;
    private Rigidbody2D _rb;

    [SerializeField] AudioSource _audioSource;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _shot.SetFalse();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _rb.velocity = (_movingLeft ? Vector2.left : _movingRight ? Vector2.right : new Vector2(0, 0)) * _moveSpeed.Value;
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        _movingLeft = context.performed;
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        _movingRight = context.performed;
    }

    public void HandleShot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_shot.Value == false)
            {
                ProjSpawning spawn = transform.GetComponentInChildren<ProjSpawning>();
                spawn.Shoot();
                _shot.SetTrue();
                _audioSource.Play();
            }
        }
    }

    public void Reset()
    {
        transform.position = new Vector2(0, transform.position.y);
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
