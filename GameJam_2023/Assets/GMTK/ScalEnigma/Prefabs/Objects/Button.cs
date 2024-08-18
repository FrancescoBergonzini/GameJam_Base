using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ScalEnigma
{
    public class Button : MonoBehaviour
    {
        public bool disappear = false;
        public GameObject target;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == Layers.Player)
            {
                target.SetActive(true);
            }

            if (other.gameObject.layer == Layers.Player && disappear == true)
            {
                target.SetActive(false);
            }
        }
    }
}

