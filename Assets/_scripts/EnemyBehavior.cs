using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public BoolVariable LeftOrRight;
    public BoolVariable Down;

    [SerializeField] FloatReference _unitScore;
    [SerializeField] FloatVariable _gameScore;
    [SerializeField] GameEvent hit;

    public bool HitWall(string s)
    {
        float posX;
        Vector2 dir;

        if(s == "left")
        {
            posX = transform.position.x - .6f;
            dir = Vector2.left;
        } 
        else
        {
            posX = transform.position.x + .6f;
            dir = Vector2.right;
        }

        Vector3 position = new Vector3(posX, transform.position.y, 0);
        //Debug.DrawRay(position, dir, Color.green, 20);

        RaycastHit2D hit = Physics2D.Raycast(position, dir, 1);
        //Debug.Log(hit.collider);

        if (hit.collider && hit.collider.CompareTag("Walls")) return true;
        else return false;
    }

    public bool EnemyInFront()
    {
        Vector3 position = new Vector3(transform.position.x, (transform.position.y - .6f), 0);
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 2f);
        //Debug.DrawRay(position, Vector2.down, Color.green, 20);
        // Debug.Log(hit.collider);
        return hit.collider;
    }

    public void Shoot()
    {
        ProjSpawning spawnScript = transform.GetComponentInChildren<ProjSpawning>();
        spawnScript.Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == gameObject) return;
        _gameScore.ApplyChange(_unitScore.Value);
        hit.Raise();
        Destroy(gameObject);
    }

    public void Move(string direction)
    {
        Vector3 move;
        if(direction == "down")
        {
            move = new Vector3(transform.position.x, transform.position.y - 1, transform.position.y);
            Down.SetFalse();
        }
        else if(direction == "left")
        {
            if (HitWall(direction))
            {
                Down.SetTrue();
                LeftOrRight.SetFalse();
            }
            move = new Vector3(transform.position.x - 1, transform.position.y, transform.position.y);
            
        }
        else
        {
            if (HitWall(direction))
            {
                Down.SetTrue();
                LeftOrRight.SetTrue();
            }
            move = new Vector3(transform.position.x + 1, transform.position.y, transform.position.y);
        }
        transform.position = move;
    }
}
