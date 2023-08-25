using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class PezzoBiscotto : PhysicsEntity
    {
        [SerializeField] float dissolveTime = 1;



        public void Release()
        {

            _rdb = gameObject.AddComponent<Rigidbody2D>();
            _rdb.useAutoMass = true;

            gameObject.layer = Layers.PezziBiscotto;
            transform.parent = null;

            //solo ora diventa fisico!
            isPhysic = true;


            transform.DOScale(Vector3.zero, dissolveTime).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); });

        }

        public override void OnLiquidEnter()
        {
            base.OnLiquidEnter();

            _rdb.gravityScale = inLiquidSpeed;
            DOTween.To(() => _rdb.velocity, x => _rdb.velocity = x, Vector2.zero, .5f);
        }

        public override void OnLiquidExit()
        {
            base.OnLiquidExit();

            DOTween.To(() => _rdb.velocity, x => _rdb.velocity = x, Vector2.zero, .1f);
            _rdb.gravityScale = outsideLiquidSpeed;

            if (!firstEnterInLiquid)
            {
                PlayParticle(ParticleType.liquid);
                firstEnterInLiquid = true;
            }
        }
    }
}