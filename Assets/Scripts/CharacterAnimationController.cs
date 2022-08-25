using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private string idleAnimationStateName;
    [SerializeField] private string moveAnimationStateName;
    [SerializeField] private string jumpAnimationStateName;
    [SerializeField] private string attackAnimationStateName;

    //[SyncVar] private bool _isIdle;
    //[SyncVar] private bool _isMoving;
    //[SyncVar] private bool _isJump;
    //[SyncVar] private bool _isAttack;

    private int _idleHash;
    private int _moveHash;
    private int _jumpHash;
    private int _attackHash;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _idleHash = Animator.StringToHash(idleAnimationStateName);
        _moveHash = Animator.StringToHash(moveAnimationStateName);
        _jumpHash = Animator.StringToHash(jumpAnimationStateName);
        _attackHash = Animator.StringToHash(attackAnimationStateName);
        
    }

    private void Start()
    {
        _animator.Play(_idleHash);
    }

    public void SetAnimationMove()
    {
        Debug.Log("Do Animation: Move!!!");
        SetAnimation(_moveHash);
    }

    public void SetAnimationJump()
    {
        Debug.Log("Do Animation: Jump!!!");
        SetAnimation(_jumpHash);
    }

    //[ServerRpc]
    private void SetAnimation(int animationStateHash)
    {
        _animator.Play(animationStateHash);
    }
    
}
