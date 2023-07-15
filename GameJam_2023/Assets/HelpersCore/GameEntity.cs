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

        public GameObject PlayParticle(System.Enum type, Vector3? position = null)
        {
            return GameManagerBase.Instance.PlayParticle(type, (position != null ? (Vector3)position : transform.position));
        }

        public void PlaySound(System.Enum type, Vector3? position = null)
        {
            GameManagerBase.Instance.PlaySound(type, (position != null ? (Vector3)position : transform.position));
        }


        #endregion
    }
}

