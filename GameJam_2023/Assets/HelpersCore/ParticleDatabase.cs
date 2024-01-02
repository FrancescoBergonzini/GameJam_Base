using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public interface IParticleDatabase
    {
        (IParticleData data, GameObject instantiated_particle) PlayParticle(System.Enum type, Vector3 position);
        (IParticleData data, GameObject instantiated_particle) PlayParticle(IParticleData data, Vector3 position);

    }

    public class ParticleDatabase<E, SE> : MonoBehaviour, IParticleDatabase
        where E : System.Enum
        where SE : System.Enum // sound enum
    {
        public ParticleData<E, SE>[] Items;


        // indexed dictionary for speed
        private Dictionary<E, ParticleData<E, SE>> _dict = null;


        private void _setup()
        {
            if (_dict != null)
                return;

            _dict = new Dictionary<E, ParticleData<E, SE>>();
            foreach (var i in Items)
            {
                _dict.Add(i.type, i);
            }
        }

        public ParticleData<E, SE> GetItem(E id)
        {
            _setup();
            return _dict[id];
        }


        public (IParticleData data, GameObject instantiated_particle) PlayParticle(Enum type, Vector3 position)
        {
            var particle = GetItem((E)type);


            if (particle != null && particle.prefab_particle != null)
            {
                //istanzia
                // TODO: instantiate under object?
                GameObject part = Instantiate(particle.prefab_particle, position, Quaternion.identity);

                if (particle.Lifetime > 0)
                    Destroy(part, particle.Lifetime);

                return (data: particle, instantiated_particle: part);
            }
            else
            {
                Debug.LogWarning($"ParticleDatabase: no particle fx for type={type}");
                return (data: particle, instantiated_particle: null);
            }
        }

        public (IParticleData data, GameObject instantiated_particle) PlayParticle(IParticleData data, Vector3 position)
        {
            if (data != null && data.GetParticlePrefab() != null)
            {
                //istanzia
                // TODO: instantiate under object?
                GameObject part = Instantiate(data.GetParticlePrefab(), position, Quaternion.identity);

                if (data.GetLifetime() > 0)
                    Destroy(part, data.GetLifetime());

                return (data: data, instantiated_particle: part);
            }
            else
            {
                Debug.LogWarning($"ParticleDatabase: no particle fx for data particle ={data}");
                return (data: data, instantiated_particle: null);
            }
        }
    }

    public interface IParticleData
    {
        float GetLifetime();

        GameObject GetParticlePrefab();

        ISoundData GetSound();

        System.Enum GetSoundType();
    }

    [Serializable]
    public class ParticleData : IParticleData
    {
        public GameObject prefab_particle;

        [Range(-1f, 20f)]
        public float Lifetime;

        // sound
        public SoundData sound_data;

        #region IParticleData

        public float GetLifetime() => Lifetime;

        public GameObject GetParticlePrefab() => prefab_particle;

        public GameObject PlayParticle(Vector3 position)
        {

            if (prefab_particle != null)
            {
                //istanzia
                // TODO: instantiate under object?
                GameObject part = GameObject.Instantiate(prefab_particle, position, Quaternion.identity);

                if (Lifetime > 0)
                    GameObject.Destroy(part, Lifetime);

                return part;
            }

            return null;

        }

        public virtual ISoundData GetSound() => sound_data;

        public virtual Enum GetSoundType()
        {
            Debug.LogWarning("ParticleData: cannot GetSoundType() on a non generic ParticleData");

            return default;
        }

        #endregion
    }


    [Serializable]
    public class ParticleData<E, SE> : ParticleData
        where E : System.Enum // particle type
        where SE : System.Enum // sound type
    {
        public E type;

        // sound
        public SE sound_type;

        public override System.Enum GetSoundType() => sound_type;
    }
}
