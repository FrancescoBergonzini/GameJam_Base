using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamCore.Brakeys_2023
{
    /// <summary>
    /// Classe usata per oggetti che interagiscono con liquidi
    /// </summary>
    public abstract class PhysicsEntity : GameEntity
    {
        protected bool inAir;

        [SerializeField] protected float outsideLiquidSpeed;
        [SerializeField] protected float inLiquidSpeed;

        [SerializeField] bool affectLiquidSurface;

        [Space]
        public bool isPhysic = false;
        protected Rigidbody2D _rdb;
        protected float velocity => isPhysic == false ? 0 : GetRigidbody().velocity.magnitude;
        private Vector3 _currentVelocity = Vector3.zero;

        [Space]
        public bool firstEnterInLiquid = false;
        public float MaxSpeed;
        public float MinSpeed;
        [Space]
        public float Smooth_time;
        public float Smooth_strenght;


        #region Triggers
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Layers.Liquido)
            {
                OnLiquidEnter();
            }
        }

        public virtual void OnLiquidEnter()
        {
            inAir = false;
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Layers.Liquido)
            {
                OnLiquidExit();
            }
        }

        public virtual void OnLiquidExit()
        {
            inAir = true;
        }
        #endregion

        #region Physic

        public virtual void FixedUpdate()
        {
            if (!isPhysic || inAir)
                return;

            if (velocity > MaxSpeed || velocity < MinSpeed)
            {
                if (velocity == 0)
                    return;

                LimitVelocity();

            }
        }

        public Rigidbody2D GetRigidbody()
        {
            if (_rdb != null)
            {
                return _rdb;
            }
            else
            {
                _rdb = GetComponent<Rigidbody2D>();
                return _rdb;
            }
                
        }


        private void LimitVelocity()
        {
            // Limita la velocità dell'oggetto alla velocità massima o minima consentita in modo graduale tramite SmoothDamp
            float targetVelocity = Mathf.Clamp(velocity, MinSpeed, MaxSpeed);

            GetRigidbody().velocity = Vector3.SmoothDamp(current: GetRigidbody().velocity,
                                             target: GetRigidbody().velocity.normalized * targetVelocity,
                                             currentVelocity: ref _currentVelocity,
                                             smoothTime: Smooth_time,
                                             Smooth_strenght);
        }

        #endregion
    }
}
