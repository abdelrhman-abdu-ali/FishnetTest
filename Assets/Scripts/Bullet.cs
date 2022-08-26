using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private bool _triggerDestroy;

    private bool _isInitialize;
    //private BulletSpawner _owner;
    //[SyncVar] private bool _isDestroyed;

    

    public override void OnStartClient()
    {
        base.OnStartClient();
        _isInitialize = true;
    }

    IEnumerator WaitInitialize()
    {
        while (!_isInitialize)
        {
            yield return null;
        }

        DestroyBullet(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out CharacterStats  stat))
        {
            stat.TakeDamage(5);
            //DestroyBullet(this);
            StartCoroutine("WaitInitialize");
        }
        
    }

    private void Update()
    {
        transform.position += new Vector3(_bulletSpeed*Time.deltaTime, 0, 0);
        _tempTime += Time.deltaTime;
        if (_tempTime >= _autoDestroyTime)
        {
            StartCoroutine("WaitInitialize");
        }

        //if (_isDestroyed)
        //{
        //    Destroy(this.gameObject);
        //}
        
    }
    
    //[ServerRpc (RequireOwnership = false)]
    private void DestroyBullet(Bullet bullet)
    {
        if (!_isInitialize)
        {
            DestroyBullet(this);
            return;
        }
        
        if (!_triggerDestroy)
        {
            _triggerDestroy = true;
            InstanceFinder.ServerManager.Despawn(_networkObject);
        }
        
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
