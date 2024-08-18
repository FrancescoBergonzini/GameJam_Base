using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalEnigma
{
    public class Edge : GameEntity
    {
        public void Awake()
        {
            SetLayerRecursively(this.gameObject, Layers.Edge);
        }
    }
}

