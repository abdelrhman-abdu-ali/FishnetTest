using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _autoDestroyTime;
    private float _tempTime;

    [SerializeField] private NetworkObject _networkObject;
    //private BulletSpawner _owner;
    //[SyncVar] private bool _isDestroyed;

    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out CharacterStats  stat))
        {
            stat.TakeDamage(5);
            DestroyBullet(this);
        }
        
    }

    private void Update()
    {
        transform.position += new Vector3(_bulletSpeed*Time.deltaTime, 0, 0);
        _tempTime += Time.deltaTime;
        if (_tempTime >= _autoDestroyTime)
        {
            DestroyBullet(this);
        }

        //if (_isDestroyed)
        //{
        //    Destroy(this.gameObject);
        //}
        
    }
    
    //[ServerRpc (RequireOwnership = false)]
    private void DestroyBullet(Bullet bullet)
    {
        InstanceFinder.ServerManager.Despawn(_networkObject);
        //_networkObject.Despawn(this.gameObject);
        //_isDestroyed = true;
        //_owner.DestroyObject(this.gameObject);
        //ServerManager.Despawn(bullet.gameObject);
        //Destroy(bullet.gameObject);
    }
    
    

    [ServerRpc]
    public void SetupOwner( BulletSpawner owner)
    {
        //_owner = owner;
    }
}
