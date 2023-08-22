using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class GameEntity : MonoBehaviour
    {

        protected virtual void Inizialize()
        {

        }

        //Manage layer
        public virtual void _changeLayer(int layer)
        {
            List<Transform> _getAllChildren(Transform parent)
            {
                parent.gameObject.layer = layer;

                List<Transform> children = new List<Transform>();

                foreach (Transform child in parent)
                {
                    //change layer
                    child.gameObject.layer = layer;

                    children.Add(child);
                    children.AddRange(_getAllChildren(child));
                }

                return children;
            }

            _getAllChildren(this.gameObject.transform);

        }

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

