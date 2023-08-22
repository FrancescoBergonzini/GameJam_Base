using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public struct Liquid_Config
    {
        public float densità;
        public float galleggibilità;
        public float riempimento; //da 1 a 3 , a metà , quasi pieno e pieno

    }

    public class Tazza : GameEntity
    {
        public Liquid_Config current_liquid;

        protected override void Inizialize()
        {
            _changeLayer(Layers.Tazza);
        }
    }
}

