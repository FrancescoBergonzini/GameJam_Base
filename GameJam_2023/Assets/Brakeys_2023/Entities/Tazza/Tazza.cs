using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Tazza : GameEntity
    {
        protected override void Inizialize()
        {
            _changeLayer(Layers.Tazza);
        }
    }
}

