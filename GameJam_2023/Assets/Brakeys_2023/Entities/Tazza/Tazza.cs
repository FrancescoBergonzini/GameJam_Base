using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public struct Liquid_Config
    {
        public float densit�; //determina la velocit� del cucchiaio 
        public float galleggibilit�; //determina la velocit� del biscotto
        public Riempimento riempimento; //da 1 a 3 , a met� , quasi pieno e pieno

    }

    public enum Riempimento
    {
        mezzo_pieno,
        tre_quarti_pieno,
        pieno
    }

    public class Tazza : GameEntity
    {
        public Liquid_Config current_liquid;
        [SerializeField] SpriteRenderer front;

        protected override void Inizialize()
        {
            _changeLayer(Layers.Tazza);
        }

        public void Setup()
        {
            front.DOFade(0, 2).SetDelay(1);
        }
    }
}

