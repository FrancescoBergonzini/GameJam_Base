using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;

namespace GMTK
{
    public class Enemy : GameEntity

    {
        public new EnemyConfig Config => base.Config as EnemyConfig;
        public new EnemyConfig Runtime => base.Runtime as EnemyConfig;

        public float time = 1;
        public Ease ease;
        private void OnTriggerEnter(Collider other)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Collider>().enabled = false;

            var sequenza = DOTween.Sequence();

            sequenza.Join(this.transform.DORotate(new Vector3(0, 0, 460), 1f, RotateMode.LocalAxisAdd).SetEase(ease));
            sequenza.Join(this.transform.DOScale(0.1f, 1f).SetEase(ease));
            sequenza.Join(this.transform.DOMove(other.transform.parent.parent.position, 1).SetEase(ease));

            sequenza.OnComplete(() => Destroy(this.gameObject));
        }
    }
}

