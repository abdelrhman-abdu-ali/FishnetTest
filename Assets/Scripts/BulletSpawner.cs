using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{
    [SerializeField]private GameObject _bulletLeftObject;
    [SerializeField]private GameObject _bulletRightObject;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            GetComponent<BulletSpawner>().enabled = false;
        }
    }

    [ServerRpc]
    public void SpawnBullet(GameObject bullet,Vector3 worldPosition,BulletSpawner spawner)
    {
        GameObject spawnedBullet = Instantiate(bullet, worldPosition, Quaternion.identity);
        ServerManager.Spawn(spawnedBullet);
    }

    public void SpawnBulletLeft(Vector3 position)
    {
        SpawnBullet(_bulletLeftObject, position, this);
    }
    
    
    
    
}
