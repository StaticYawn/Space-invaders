using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSpawning : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
