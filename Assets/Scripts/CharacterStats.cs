using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class CharacterStats : NetworkBehaviour
{
    [field: SyncVar] private int _health { get; set; }


    [SyncVar] private int _maxHealth;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            GetComponent<CharacterStats>().enabled = false;
            return;
        }

        SetupHealth(this, 100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(5);
        }
    }


    [ServerRpc]
    public void SetupHealth(CharacterStats statsOwner,int maxHealth)
    {
        statsOwner._maxHealth = maxHealth;
        statsOwner._health = maxHealth;
    }
    
    
    [ServerRpc]
    private void UpdateHealth(CharacterStats statsOwner,int healthChanges)
    {
        statsOwner._health += healthChanges;
    }

    public void TakeDamage(int amountDamage)
    {
        UpdateHealth(this, -amountDamage);
    }
}
