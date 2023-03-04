using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPixelBehavior : MonoBehaviour
{

    float _radius = 0.0625f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InnerCircleCast();
        OutterCircleCast();
        Destroy(gameObject);
    }

    private void InnerCircleCast()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _radius, new Vector2(0,0));
        foreach(RaycastHit2D hit in hits)
        {
            Destroy(hit.collider.gameObject);
        }
    }

    private void OutterCircleCast()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _radius * 3, new Vector2(0, 0));
        foreach (RaycastHit2D hit in hits)
        {
            float rnd = Random.Range(0, 15);
            if(rnd > 15 /2)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
