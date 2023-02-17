using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private FloatReference MoveSpeed;
    [SerializeField] BoolVariable shot;
    private bool MovingLeft = false;
    private bool MovingRight = false;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shot.SetFalse();
    }

    void Update()
    {
        rb.velocity = (MovingLeft ? Vector2.left : MovingRight ? Vector2.right : new Vector2(0, 0)) * MoveSpeed.Value;
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        MovingLeft = context.performed;
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        MovingRight = context.performed;
    }

    public void HandleShot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (shot.Value == false)
            {
                ProjSpawning spawn = transform.GetComponentInChildren<ProjSpawning>();
                spawn.Shoot();
                shot.SetTrue();
            }
        }
    }
}
