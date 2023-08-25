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
        public float resistenza_al_liquido;
        public float danno_da_impatto; // colpire un biscotto con una velocità alta fa più danno

        //o meglio avere direttamente un altro cucchiaio?
        public float dimensione;
    }

    public class Cucchiaio : PhysicsEntity
    {
        Vector2 inputForce;

        Collider2D _col;

        [Space]
        public Cucchiaio_Config current_config;

        protected enum Orientamento
        {
            none,
            Left,
            Right
        }


        //inLiquid determina la velocità 
        //se fuori da liquidi tazza si muova a velocità fissa
        //se dentro liquido è dato da velocità di movimento * 
        public bool inLiquid = false;

        protected Orientamento current_orientamento = Orientamento.none;


        private void Awake()
        {
            //qua se è già in scena, se va spawnato mettiamolo nel Create
            Inizialize();
        }

        protected override void Inizialize()
        {

            if (_col == null)
                _col = GetComponent<Collider2D>();

            //parte a destra
            current_orientamento = Orientamento.Left;

            _changeLayer(Layers.Cucchiaio);
        }

        private void Update()
        {
            //input 
            inputForce = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            RotateLeft();

            RotateRight();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (inLiquid)
                ManageMovementInLiquid();

            if (!inLiquid)
                ManageMovementInAir();
        }

        #region Input
        public void ManageMovementInLiquid()
        {

            //Debug.Log(horizontal);
            //Debug.Log(vertical);

            //Vector2 inputForce = new Vector2(horizontal, vertical);
            float inputMagnitude = inputForce.magnitude; // Calcola la magnitudine dell'input

            // Calcola la velocità finale usando CalculateSpeed
            float finalVelocity = CalculateSpeed(inputMagnitude: inputMagnitude,
                                                 fluidDensity: GameManager.instance.current_level_tazza.current_liquid.densità,
                                                 objectDensity: this.current_config.resistenza_al_liquido);

            // Aggiungi la forza dell'input al rigidbody usando AddForce
            _rdb.AddForce(inputForce * current_config.velocità * finalVelocity, ForceMode2D.Force);
        }

        public void ManageMovementInAir()
        {
            _rdb.AddForce(inputForce * current_config.velocità, ForceMode2D.Force);
        }

        //Questo metodo semplifica ulteriormente la resistenza al fluido e si basa sull'idea generale che più è alta la densità del fluido,
        //maggiore sarà la resistenza.
        private float CalculateSpeed(float inputMagnitude, float fluidDensity, float objectDensity)
        {
            // Calcola la velocità finale in base all'input, la densità del fluido e la densità del cucchiaio
            float finalVelocity = inputMagnitude * (1 - (fluidDensity / objectDensity));

            return finalVelocity;
        }

        public void RotateLeft()
        {
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

        }

        public void RotateRight()
        {
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

        }

        #endregion
    }

}

