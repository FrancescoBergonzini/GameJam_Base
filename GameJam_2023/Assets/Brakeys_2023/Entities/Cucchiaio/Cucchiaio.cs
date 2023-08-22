using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Cucchiaio : GameEntity
    {
        Rigidbody2D _rdb;
        Collider2D _col;

        protected enum Orientamento
        {
            none,
            Left,
            Right
        }

        protected Orientamento current_orientamento = Orientamento.none;


        private void Awake()
        {
            //qua se è già in scena, se va spawnato mettiamolo nel Create
            Inizialize();
        }

        protected override void Inizialize()
        {
            //parte a destra
            current_orientamento = Orientamento.Left;

            _changeLayer(Layers.Cucchiaio);
        }

        private void Update()
        {
            ManageMovement();

            RotateLeft();

            RotateRight();
        }

        #region Input
        public void ManageMovement()
        {
            //gestisci input per controller e wasd
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Debug.Log(horizontal);
            Debug.Log(vertical);
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

