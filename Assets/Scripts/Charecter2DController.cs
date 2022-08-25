using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using FishNet.Example.Prediction.CharacterControllers;

public class Charecter2DController : NetworkBehaviour
{
    [SerializeField] private Transform _charecterTransform;
    [SerializeField] private BoxCollider2D _character2DBoxCollider;
    [SerializeField] private float _extraHeight = 0.1f;
    private bool _isOnGround;

    private void Update()
    {
        _isOnGround = IsGround();
    }

    public void Move(Vector3 magnitude )
    {
        if (_isOnGround)
        {
            if (magnitude.y < 0)
            {
                magnitude = new Vector3(magnitude.x, 0, magnitude.z);
            }
            _charecterTransform.position += magnitude;
        }
        else
        {
            _charecterTransform.position += magnitude;
        }
        
    }

    private bool IsGround()
    {

        Physics2D.Raycast(_character2DBoxCollider.bounds.center, Vector2.down,
            _character2DBoxCollider.bounds.extents.y + _extraHeight);
        Color rayColor;
        Debug.DrawRay(_character2DBoxCollider.bounds.center, Vector2.down*
            (_character2DBoxCollider.bounds.extents.y + _extraHeight));
        return true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        /*Debug.Log("Touch something");
        Debug.Log("CompareTag "+col.gameObject.tag + "obj name:"+col.gameObject.name);
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touch ground");
            
            _isOnGround = true;
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touch ground");
            
            //_isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("leave ground");
            //_isOnGround = false;
        }
    }
}
