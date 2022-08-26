using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private string idleAnimationBoolName;
    [SerializeField] private string moveAnimationBoolName;
    [SerializeField] private string jumpAnimationBoolName;
    [SerializeField] private string attackAnimationBoolName;

    private bool _isIdle;
    private bool _isMoving;
    private bool _isJump;
    private bool _isAttack;

    private int _idleHash = 0;
    private int _moveHash = 0;
    private int _jumpHash = 0;
    private int _attackHash = 0;

    private int[] listBool;
    private int tempHash =0;
    


    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _idleHash = Animator.StringToHash(idleAnimationBoolName);
        _moveHash = Animator.StringToHash(moveAnimationBoolName);
        _jumpHash = Animator.StringToHash(jumpAnimationBoolName);
        _attackHash = Animator.StringToHash(attackAnimationBoolName);
        listBool = new int[] {_idleHash, _moveHash, _jumpHash, _attackHash};

    }

    private void Start()
    {
        //_animator.Play(_idleHash);
    }

    public void SetAnimationMove()
    {
        Debug.Log("Do Animation: Move!!!");
        SetAnimation(_moveHash);
    }
    
    public void SetAnimationIdle()
    {
        Debug.Log("Do Animation: idle!!!");
        SetAnimation(_idleHash);
    }

    public void SetAnimationJump()
    {
        Debug.Log("Do Animation: Jump!!!"+_jumpHash);
        SetAnimation(_jumpHash);
    }

    //[ServerRpc]
    private void SetAnimation(int animationStateHash)
    {
        
        //_animator.Play(animationStateHash);
        /*if (animationStateHash == _idleHash && _isIdle)
        {
            return;
        }
        
        if (animationStateHash == _moveHash && _isMoving)
        {
            return;
        }
        
        if (animationStateHash == _jumpHash && _isJump)
        {
            return;
        }*/

        if (animationStateHash == tempHash)
        {
            return;
        }

        tempHash = animationStateHash;
        Debug.Log("Pass through return");


        if (animationStateHash == _idleHash)
        {
            _isIdle = true;
            _isMoving = false;
            _isJump = false;
        }
        
        if (animationStateHash == _moveHash)
        {
            _isIdle = false;
            _isMoving = true;
            _isJump = false;
        }
        
        if (animationStateHash == _jumpHash)
        {
            _isIdle = false;
            _isMoving = false;
            _isJump = true;
        }

        ResetAnimationBool();
        _animator.SetBool(animationStateHash,true);
        
        //_animator.Play(animationStateHash);
        
    }

    private void ResetAnimationBool()
    {
        int boolLength = listBool.Length;
        for (int i = 0; i < boolLength; i++)
        {
            _animator.SetBool(listBool[i],false);
        }
       
    }
    
}
