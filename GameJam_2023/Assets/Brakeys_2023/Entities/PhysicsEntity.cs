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

        protected UnityEvent onLiquidEnter = new UnityEvent();
        protected UnityEvent onLiquidExit = new UnityEvent();

        #region Triggers
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Layers.Liquido)
            {
                inAir = false;
                onLiquidEnter?.Invoke();
                if(affectLiquidSurface) collision.GetComponent<MilkSurface>().AddForce(transform.position.x * -20);
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Layers.Liquido)
            {
                inAir = true;
                onLiquidExit?.Invoke();
            }
        }
        #endregion
    }
}
