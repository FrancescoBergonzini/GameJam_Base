using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using DG.Tweening;

namespace ScalEnigma
{
    public class ScanlEnigma : GameManagerBase
    {
        public new static ScanlEnigma Instance;

        public void Awake()
        {
            Instance = this;
        }

        [Space]
        public float swich_duration;
        public Ease swich_ease;
    }
}

