using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class test_particle : GameEntity
    {
        [SerializeField] ParticleData particleData;
        [SerializeField] SoundData soundData;

        private void OnEnable()
        {
            PlayParticle(particleData);
            PlaySound(soundData);
        }
    }
}

