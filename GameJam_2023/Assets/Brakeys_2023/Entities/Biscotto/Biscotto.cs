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

        protected enum State
        {
            none,
            not_interactable,
            interactable
        }

        protected State current_State = State.none;


        public static int ActiveInGame = 0;

        public static Biscotto Create(Biscotto prefab, Vector3 position, Quaternion rotation, Vector2 force, Transform parent)
        {
            Biscotto _biscotto = Instantiate(prefab, position, rotation, parent);

            _biscotto.Inizialize();

            ActiveInGame++;

            return _biscotto;
        }


        public void Inizialize()
        {
            if (_rdb == null)
                _rdb = GetComponent<Rigidbody2D>();

            if (_col == null)
                _col = GetComponent<Collider>();

            //Sempre 100 di base?
            SetIntegrity(100f);

            //imposta il numero iniziale di pezzi
            startCookiePiecesCount = cookiePieces.Count;
        }

        private void Start()
        {
            Inizialize();
            StartCoroutine(TestIntegrtity());
        }

        /// <summary>
        /// solo per test
        /// </summary>
        /// <returns></returns>
        IEnumerator TestIntegrtity()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                ModifyIntegrity(-1);
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
                removedPiece.layer = LayerMask.NameToLayer("PezziBiscotto");
                removedPiece.transform.parent = null;
                var pieceRigidbody = removedPiece.AddComponent<Rigidbody2D>();
                pieceRigidbody.gravityScale = -0.8f;
                cookiePieces.RemoveAt(0);
            }
        }

        #endregion

        #region State


        #endregion


    }
}

