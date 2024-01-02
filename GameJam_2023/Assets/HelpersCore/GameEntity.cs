using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class GameEntity : MonoBehaviour
    {
        #region Helpers

        public GameObject PlayParticleMute(System.Enum type, Vector3? position = null)
        {
            return GameManagerBase.Instance.PlayParticleMute(type, (position != null ? (Vector3)position : transform.position));
        }

        public GameObject PlayParticle(System.Enum type, Vector3? position = null, float? z_pos = null)
        {
            return GameManagerBase.Instance.PlayParticle(type, (position != null ? (Vector3)position : transform.position), z_pos: z_pos);
        }

        public GameObject PlayParticle(IParticleData data, Vector3? position = null, float? z_pos = null)
        {
            return GameManagerBase.Instance.PlayParticle(data, (position != null ? (Vector3)position : transform.position), z_pos: z_pos);
        }

        public GameObject PlayParticleMute(IParticleData data, Vector3? position = null, float? z_pos = null)
        {
            return GameManagerBase.Instance.PlayParticleMute(data, (position != null ? (Vector3)position : transform.position), z_pos: z_pos);
        }

        public void PlaySound(System.Enum type, Vector3? position = null, float? volume_override = null)
        {
            GameManagerBase.Instance.PlaySound(type, position: (position != null ? (Vector3)position : transform.position), volume: volume_override);
        }

        public void PlaySound(ISoundData sound, Vector3? position = null, float? volume_override = null)
        {
            GameManagerBase.Instance.PlaySound(sound, position: (position != null ? (Vector3)position : transform.position), volume: volume_override);
        }


        #endregion
    }
}

