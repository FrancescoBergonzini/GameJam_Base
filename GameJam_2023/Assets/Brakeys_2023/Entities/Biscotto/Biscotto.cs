using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Biscotto : PhysicsEntity
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

        [SerializeField, ReorderableList] List<PezzoBiscotto> cookiePieces;
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

            _biscotto.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360f));

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

            //aggiungi listener per entrata/uscita da liquido
            onLiquidEnter.AddListener(() =>
            {
                _rdb.gravityScale = inLiquidSpeed;
                DOTween.To(() => _rdb.velocity, x => _rdb.velocity = x, Vector2.zero, .5f);
                StartCoroutine(Deteriorate());
            });

            onLiquidExit.AddListener(() =>
            {
                _rdb.gravityScale = outsideLiquidSpeed;
                StopCoroutine(nameof(Deteriorate));
            });
        }


<<<<<<< HEAD
        private void Update()
        {
            if (InAir)
            {

            }


            if (!InAir)
            {

            }
        }

=======
        /// <summary>
        /// solo per test
        /// </summary>
        /// <returns></returns>
        IEnumerator Deteriorate(float startDelay = 3)
        {
            yield return new WaitForSeconds(startDelay);
            while (true)
            {
                yield return new WaitForSeconds(1);
                ModifyIntegrity(-0.3f);
            }
        }

        private void SetFallingSpeed()
        {

        }
>>>>>>> 32fb162f246b58d24f550913f2be70655bbd8aba

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
            //controlla se l'integrità è minore della soglia per rimuovere un pezzo
            if (Integrity < (100f / startCookiePiecesCount) * cookiePieces.Count)
            {
                var removedPiece = cookiePieces[0];
                removedPiece.Release();

                //TODO: useremo poi una classe apposta in moda da evitare il dispendioso AddComponent
                cookiePieces.RemoveAt(0);
            }
        }

        #endregion





    }
}

