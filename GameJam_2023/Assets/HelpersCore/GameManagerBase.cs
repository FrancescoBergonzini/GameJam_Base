using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class GameManagerBase : GameEntity
    {
        // References to sound and particle databases (runtime)
        private ISoundDatabase SoundDatabase;
        private IParticleDatabase ParticleDatabase;

        public static GameManagerBase Instance;

        private void Awake()
        {
            OnAwake();
        }
        public virtual void OnAwake()
        {
            if (!this.enabled)
                return;

            Instance = this;

            SoundDatabase = GetComponent<ISoundDatabase>();
            if (SoundDatabase == null)
            {
                Debug.LogError("GameManagerBase needs a SoundDatabase");
            }

            ParticleDatabase = GetComponent<IParticleDatabase>();
            if (ParticleDatabase == null)
            {
                Debug.LogError("GameManagerBase needs a ParticleDatabase");
            }
        }

        #region Particles

        public GameObject PlayParticleMute(System.Enum type, Vector3 position)
        {
            var p = ParticleDatabase.PlayParticle(type, position);

            return p.instantiated_particle;
        }

        public GameObject PlayParticle(System.Enum type, Vector3 position)
        {
            var p = ParticleDatabase.PlayParticle(type, position);
            System.Enum sound_type = p.data.GetSound();

            if ((int)(object)sound_type != 0)
            {
                PlaySound(sound_type, position);
            }

            return p.instantiated_particle;
        }

        public void PlaySound(System.Enum type, Vector3? position)
        {
            SoundDatabase.Play(type, position);
        }


        #endregion
    }
}


