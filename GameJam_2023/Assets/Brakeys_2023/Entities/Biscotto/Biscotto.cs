using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Biscotto : PhysicsEntity
    {
        Collider _col;

        [Space]
        [SerializeField] float _integrity;
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


        //
        [Space]
        public float danno_integrità_second; //influenzato dal liquido 

        [Space]
        [SerializeField, ReorderableList] List<PezzoBiscotto> cookiePieces;
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

            _biscotto.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360f));

            ActiveInGame++;

            return _biscotto;
        }


        protected override void Inizialize()
        {

            if (_col == null)
                _col = GetComponent<Collider>();

            //
            inAir = true;

            //Sempre 100 di base?
            SetIntegrity(100f);

            //imposta il numero iniziale di pezzi
            startCookiePiecesCount = cookiePieces.Count;

            //layer
            _changeLayer(Layers.Biscotto);

        }


        public override void FixedUpdate()
        {
            //limit velocity
            base.FixedUpdate();
        }


        /// <summary>
        /// solo per test
        /// </summary>
        /// <returns></returns>
        IEnumerator deteriorate_cr(float startDelay = 3)
        {
            yield return new WaitForSeconds(startDelay);

            while (true)
            {
                yield return new WaitForSeconds(1);

                ModifyIntegrity(danno_integrità_second);
            }
        }

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


        public override void OnLiquidEnter()
        {
            base.OnLiquidEnter();

            GetRigidbody().gravityScale = inLiquidSpeed * UnityEngine.Random.Range(1, 1.5f);

            //lerp velocità, non più 0 ma piccolissima
            DOTween.To(() => GetRigidbody().velocity, x => GetRigidbody().velocity = x, GetRigidbody().velocity * 0.10f, .5f);

            //applica forza che li fa risalire un attimo
            if (!firstEnterInLiquid)
            {
                GetRigidbody().AddForce(Vector2.up * UnityEngine.Random.Range(1f, 2.5f), ForceMode2D.Impulse);
                firstEnterInLiquid = true;
            }

            //particle
            PlayParticle(ParticleType.liquid);

            //start deterioramento
            StartCoroutine(deteriorate_cr());
        }

        public override void OnLiquidExit()
        {
            base.OnLiquidExit();

            GetRigidbody().gravityScale = outsideLiquidSpeed;

            StopCoroutine(nameof(deteriorate_cr));
        }

    }
}

