using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

namespace GameJamCore.Brakeys_2023
{
    [Serializable]
    public struct Cucchiaio_Config
    {

        [Header("Cucchiaio caratteristiche")]
        public float velocità;
        public float velocità_in_liquido;
        public float danno_da_impatto; // colpire un biscotto con una velocità alta fa più danno

        //o meglio avere direttamente un altro cucchiaio?
        public float dimensione;
    }

    public class Cucchiaio : PhysicsEntity
    {
        Vector2 inputForce;

        Collider2D _col;

        [Space]
        public Transform cucchiaio_punta;

        [Space]
        public Cucchiaio_Config current_config;

        [Space]
        public float rotationSpeed = 300f;
        public float maxRotation = 25f;
        public float minRotation = -25f;


        private void Awake()
        {
            //qua se è già in scena, se va spawnato mettiamolo nel Create
            Inizialize();
        }

        protected override void Inizialize()
        {

            if (_col == null)
                _col = GetComponent<Collider2D>();

            _changeLayer(Layers.Cucchiaio);
        }

        private void Update()
        {
            //input 
            inputForce = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));

            //Rotate

        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (inAir)
                ManageMovementInAir();
            else
                ManageMovementInLiquid();

            ManageRotation();
        }

        #region Input
        public void ManageMovementInLiquid()
        {
            if (inputForce != Vector2.zero)
            {
                GetRigidbody().AddForce(inputForce * current_config.velocità_in_liquido * outsideLiquidSpeed, ForceMode2D.Force);
            }
        }

        public void ManageMovementInAir()
        {
            if(inputForce != Vector2.zero)
            {
                GetRigidbody().AddForce(inputForce * current_config.velocità * inLiquidSpeed, ForceMode2D.Force);
            }

            

        }

        public override void OnLiquidEnter()
        {
            base.OnLiquidEnter();


            //particle
            PlayParticle(ParticleType.liquid, cucchiaio_punta.transform.position);

            GetRigidbody().drag = 1f;
        }

        public override void OnLiquidExit()
        {
            base.OnLiquidExit();

            GetRigidbody().drag = 2f;
        }

        public void ManageRotation()
        {
            /*
            var keycord_rotate_left = KeyCode.Q;
            var controller_rotate_left = KeyCode.Joystick1Button4;

            if(UnityEngine.Input.GetKeyDown(keycord_rotate_left) || Input.GetKeyDown(controller_rotate_left))
            {
                //cambia stato orientamento e ruota 
                if (current_orientamento == Orientamento.Left) return;

                //logica rotazione...

                current_orientamento = Orientamento.Left;

                return;
            }

            var keycord_rotate_right = KeyCode.E;
            var controller_rotate_right = KeyCode.Joystick1Button5;

            if (UnityEngine.Input.GetKeyDown(keycord_rotate_right) || Input.GetKeyDown(controller_rotate_right))
            {
                //cambia stato orientamento e ruota 
                if (current_orientamento == Orientamento.Right) return;

                //logica rotazione...

                current_orientamento = Orientamento.Right;

                return;
            }
            */

            float input = 0;

            //pos
            var keycord_rotate_left = KeyCode.Q;
            var controller_rotate_left = KeyCode.Joystick1Button4;

            //neg
            var keycord_rotate_right = KeyCode.E;
            var controller_rotate_right = KeyCode.Joystick1Button5;

            if (UnityEngine.Input.GetKey(keycord_rotate_left) || Input.GetKey(controller_rotate_left))
                input = -1f;

            if (UnityEngine.Input.GetKey(keycord_rotate_right) || Input.GetKey(controller_rotate_right))
                input = 1f;

            if (input == 0)
                return;

            float desiredRotation = GetRigidbody().rotation + input * rotationSpeed * Time.deltaTime;
            desiredRotation = Mathf.Clamp(desiredRotation, minRotation, maxRotation);

            GetRigidbody().SetRotation(desiredRotation);
        }


        #endregion
    }

}

