using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalEnigma
{
    public class OnMouseRedirector : MonoBehaviour
    {
        public Action OnMouseOnObject;
        public Action OnMouseExitFromObject;

        private void OnMouseEnter()
        {
            OnMouseOnObject?.Invoke();
        }

        private void OnMouseExit()
        {
            OnMouseExitFromObject?.Invoke();
        }

    }
}

