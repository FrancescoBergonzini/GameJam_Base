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



        [Space]
        public float UpperForce = 1f;


        protected enum State
        {
            none,
            not_interactable,
            interactable,
            going_to_point
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

        private void Update()
        {
            if (_integrity <= 0)
                KillMe();
        }

        public void KillMe()
        {
            if (this.gameObject != null)
                ActiveInGame--;

            Destroy(this.gameObject);
        }

        public override void FixedUpdate()
        {
            //limit velocity
            base.FixedUpdate();
        }


        bool enable_deteriorate = false;
        public void StartDeteriorate()
        {
            if (enable_deteriorate == true)
                return;

            StartCoroutine(deteriorate_cr());
            enable_deteriorate = true;
        }


        /// <summary>
        /// solo per test
        /// </summary>
        /// <returns></returns>
        IEnumerator deteriorate_cr()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));

            while (true)
            {
                yield return new WaitForSeconds(danno_integrità_second);

                if (inAir)
                {
                    yield break;
                }
                else
                {
                    ModifyIntegrity(-0.1f);
                }
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

                GetRigidbody().mass -= GetRigidbody().mass / startCookiePiecesCount;
            }
        }

        public float ConvertIntegrityToScore()
        {
            return this.Integrity;
        }


        public override void OnLiquidEnter()
        {
            base.OnLiquidEnter();

            GetRigidbody().gravityScale = inLiquidSpeed * UnityEngine.Random.Range(1, 2f);

            //lerp velocità, non più 0 ma piccolissima
            DOTween.To(() => GetRigidbody().velocity, x => GetRigidbody().velocity = x, GetRigidbody().velocity * 0.10f, .5f);

            //applica forza che li fa risalire un attimo
            if (!firstEnterInLiquid)
            {
                GetRigidbody().AddForce(Vector2.up * (UpperForce + UnityEngine.Random.Range(- UpperForce * 0.5f, UpperForce * 0.5f)), ForceMode2D.Impulse);
                firstEnterInLiquid = true;
            }


            //particle
            PlayParticle(ParticleType.liquid);
        }

        public override void OnLiquidExit()
        {
            base.OnLiquidExit();

            GetRigidbody().gravityScale = outsideLiquidSpeed;
        }

        public void Grab(Pinza pinza)
        {
            _rdb.simulated = false;
            transform.parent = pinza.transform;
            transform.DOLocalMoveX(0, 0.5f);
            inAir = false;
        }

        public void ProcessGrab()
        {
            transform.parent = null;
            var jumpPosition = new Vector2(UnityEngine.Random.Range(3.5f, 5f), UnityEngine.Random.Range(-2f, -3f));
            transform.DOJump(jumpPosition, 3, 1, 2).OnComplete(()=> { KillMe(); });
            PlayParticle(ParticleType.stars);
        }
    }
}

