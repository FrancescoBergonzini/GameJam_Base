using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class BasePlayer : GameEntity
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayParticle(ParticleType.test_visual);
        }

        
    }
}


