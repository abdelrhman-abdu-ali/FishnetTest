using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;
using FishNet.Example.Prediction.CharacterControllers;
using FishNet.Object;
using FishNet.Object.Prediction;
using UnityEngine.Serialization;

public class Character2DControllerPrediction : NetworkBehaviour
{
    #region Types.
        public struct MoveData
        {
            public float Horizontal;
            public float Jump;
        }
        public struct ReconcileData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public ReconcileData(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }
        #endregion

        #region Serialized.
        [SerializeField]
        private float _moveRate = 5f;
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _jumpSpeed = 5f;
        [SerializeField]
        private float _jetPackSpeed = 5f;
        #endregion

        #region Private.
        private Charecter2DController _characterController;
        #endregion

        private void Awake()
        {
            InstanceFinder.TimeManager.OnTick += TimeManager_OnTick;
            _characterController = GetComponent<Charecter2DController>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();            
            _characterController.enabled = (base.IsServer || base.IsOwner);
        }

        private void OnDestroy()
        {
            if (InstanceFinder.TimeManager != null)
            {
                InstanceFinder.TimeManager.OnTick -= TimeManager_OnTick;
            }
        }

        private void TimeManager_OnTick()
        {
            if (base.IsOwner)
            {
                Reconciliation(default, false);
                CheckInput(out Character2DControllerPrediction.MoveData md);
                Move(md, false);
            }
            if (base.IsServer)
            {
                Move(default, true);
                Character2DControllerPrediction.ReconcileData rd = new Character2DControllerPrediction.ReconcileData(transform.position, transform.rotation);
                Reconciliation(rd, true);
            }
        }

        private void CheckInput(out Character2DControllerPrediction.MoveData md)
        {
            md = default;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float jump = Input.GetAxisRaw("Jump");

            if (horizontal == 0f && jump == 0f)
                return;

            md = new Character2DControllerPrediction.MoveData()
            {
                Horizontal = horizontal,
                Jump = jump
            };
        }

        [Replicate]
        private void Move(Character2DControllerPrediction.MoveData md, bool asServer, bool replaying = false)
        {
            Vector3 move = new Vector3(md.Horizontal* _moveSpeed, md.Jump*_jumpSpeed, 0) + new Vector3(0f, Physics.gravity.y, 0f);
            //move = move.normalized;
            Debug.Log("Jump:"+md.Jump*_jumpSpeed+"move:"+move);
            _characterController.Move(move* _moveRate  * (float)base.TimeManager.TickDelta);
        }

        [Reconcile]
        private void Reconciliation(Character2DControllerPrediction.ReconcileData rd, bool asServer)
        {
            transform.position = rd.Position;
            transform.rotation = rd.Rotation;
        }


    }