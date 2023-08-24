using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class PezzoBiscotto : PhysicsEntity
    {
        Rigidbody2D _rdb;

        [SerializeField] float dissolveTime = 5;

        //controlla se ha creato le particelle dopo essere uscito dal liquido
        bool hasSplashed = false;

        protected override void Inizialize()
        {

        }

        public void Release()
        {
            _rdb = gameObject.AddComponent<Rigidbody2D>();
            _rdb.useAutoMass = true;

            gameObject.layer = Layers.PezziBiscotto;
            transform.parent = null;

            transform.DOScale(Vector3.zero, dissolveTime).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); });

            onLiquidEnter.AddListener(() =>
            {
                _rdb.gravityScale = inLiquidSpeed;
                DOTween.To(() => _rdb.velocity, x => _rdb.velocity = x, Vector2.zero, .5f);
            });

            onLiquidExit.AddListener(() =>
            {
                DOTween.To(() => _rdb.velocity, x => _rdb.velocity = x, Vector2.zero, .1f);
                _rdb.gravityScale = outsideLiquidSpeed;
                if (!hasSplashed)
                {
                    Brakeys_ParticleManager test = FindObjectOfType<Brakeys_ParticleManager>();
                    test.PlayParticle(ParticleType.liquid, transform.position);
                    hasSplashed = true;
                }
            });
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}