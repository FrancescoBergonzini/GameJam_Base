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
        public float resistenza_al_liquido;
        public float danno_da_impatto;

        //o meglio avere direttamente un altro cucchiaio?
        public float dimensione;
    }

    public class Cucchiaio : GameEntity
    {
        Vector2 inputForce;

        Rigidbody2D _rdb;
        Collider2D _col;

        [Space]
        public Cucchiaio_Config current_config;

        protected enum Orientamento
        {
            none,
            Left,
            Right
        }


        //inLiquid determina la velocit� 
        //se fuori da liquidi tazza si muova a velocit� fissa
        //se dentro liquido � dato da velocit� di movimento * 
        public bool inLiquid = false;

        protected Orientamento current_orientamento = Orientamento.none;


        private void Awake()
        {
            //qua se � gi� in scena, se va spawnato mettiamolo nel Create
            Inizialize();
        }

        protected override void Inizialize()
        {
            if (_rdb == null)
                _rdb = GetComponent<Rigidbody2D>();

            if (_col == null)
                _col = GetComponent<Collider2D>();

            //parte a destra
            current_orientamento = Orientamento.Left;

            _changeLayer(Layers.Cucchiaio);
        }

        private void Update()
        {
            //input 
            //inputForce
            RotateLeft();

            RotateRight();
        }


        private void FixedUpdate()
        {
            if (inLiquid)
                ManageMovementInLiquid();
        }

        #region Input
        public void ManageMovementInLiquid()
        {

            //Debug.Log(horizontal);
            //Debug.Log(vertical);

            //Vector2 inputForce = new Vector2(horizontal, vertical);
            float inputMagnitude = inputForce.magnitude; // Calcola la magnitudine dell'input

            // Calcola la velocit� finale usando CalculateSpeed
            float finalVelocity = CalculateSpeed(inputMagnitude: inputMagnitude,
                                                 fluidDensity: GameManager.instance.current_level_tazza.current_liquid.densit�,
                                                 objectDensity: this.current_config.resistenza_al_liquido);

            // Modifica i valori dell'input in base alla velocit� finale
            inputForce *= finalVelocity;
            //calcolo verticale

            // Aggiungi la forza dell'input al rigidbody usando AddForce
            _rdb.AddForce(inputForce);

        }

        public void ManageMovementInAir()
        {

        }

        //Questo metodo semplifica ulteriormente la resistenza al fluido e si basa sull'idea generale che pi� � alta la densit� del fluido,
        //maggiore sar� la resistenza.
        private float CalculateSpeed(float inputMagnitude, float fluidDensity, float objectDensity)
        {
            // Calcola la velocit� finale in base all'input, la densit� del fluido e la densit� del cucchiaio
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

