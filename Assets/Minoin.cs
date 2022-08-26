using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;

public class Minoin : NetworkBehaviour
{
    [SyncVar(Channel = Channel.Reliable, ReadPermissions = ReadPermission.Observers, SendRate = 0.1f, OnChange = nameof(Update))]
    private Transform transform;
    bool startMove=false;

    private void Start()
    {
        transform = gameObject.transform;


    }
    void Update()
    {
        if(startMove)
        {

            transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
            if (transform.position.x > 10)
            {
                transform.position = new Vector3(-9, -1, 0);

            }
            gameObject.transform.position = transform.position;
        }
       
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        startMove = true;
    }


}
