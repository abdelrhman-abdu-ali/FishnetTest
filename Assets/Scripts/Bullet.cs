using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    private void Update()
    {
        transform.position += new Vector3(_bulletSpeed*Time.deltaTime, 0, 0);
    }
}
