using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSpawning : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    public void Shoot()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
    }
}
