using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{

    public class Biscotto : GameEntity
    {
        Rigidbody2D _rdb;
        Collider _col;

        [Space]
        float _integrity;
        public float Integrity
        {
            get { return _integrity; }

            //l'integrity va da 0 a 100
            protected set
            {
                _integrity = value;

                if (_integrity < 0) _integrity = 0;
                if (_integrity > 100) _integrity = 100;

            }
        }

        [SerializeField, ReorderableList] List<GameObject> cookiePieces;
        int startCookiePiecesCount;

        //
        [Space]
        public float velocità_di_caduta_in_fluido; //influenzata dalla densità del liquido
        public float velocità_di_deterioramento; //influenzato dal liquido 

        protected enum State
        {
            none,
            not_interactable,
            interactable
        }

        protected State current_State = State.none;

        public bool InAir;

        public static int ActiveInGame = 0;

        public static Biscotto Create(Biscotto prefab, Vector3 position, Quaternion rotation, Vector2 force, Transform parent)
        {
            Biscotto _biscotto = Instantiate(prefab, position, rotation, parent);

            _biscotto.Inizialize();

            ActiveInGame++;

            return _biscotto;
        }


        protected override void Inizialize()
        {
            if (_rdb == null)
                _rdb = GetComponent<Rigidbody2D>();

            if (_col == null)
                _col = GetComponent<Collider>();

            //Sempre 100 di base?
            SetIntegrity(100f);

            //imposta il numero iniziale di pezzi
            startCookiePiecesCount = cookiePieces.Count;

            //
            current_State = State.interactable;

            //layer
            _changeLayer(Layers.Biscotto);
        }


        private void Update()
        {
            if (InAir)
            {

            }


            if (!InAir)
            {

            }
        }


        #region Force

        public Rigidbody2D GetRigidbody()
        {
            if (_rdb != null) return _rdb;
            else return null;
        }

        public void OnForce()
        {
            //manage force using this...
        }

        #endregion

        #region Integrity

        public void SetIntegrity(float value)
        {
            Integrity = value;
        }

        public void ModifyIntegrity(float value)
        {
            Integrity += value;
            Debug.Log(Integrity);
            //controlla se l'integrità è minore della soglia per rimuovere un pezzo
            if (Integrity < (100f / startCookiePiecesCount) * cookiePieces.Count)
            {
                var removedPiece = cookiePieces[0];
                removedPiece.layer = Layers.PezziBiscotto;
                removedPiece.transform.parent = null;

                //TODO: useremo poi una classe apposta in moda da evitare il dispendioso AddComponent
                var pieceRigidbody = removedPiece.AddComponent<Rigidbody2D>();
                pieceRigidbody.gravityScale = -0.8f;
                cookiePieces.RemoveAt(0);
            }
        }

        #endregion



    }
}

