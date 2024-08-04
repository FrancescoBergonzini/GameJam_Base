using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class InterestPoint : MonoBehaviour
    {
        public static event Action<InterestPoint> OnInterestPointTriggerEnter;

        public string Name => this.gameObject.name;
        private void OnTriggerEnter(Collider other)
        {
            if(OnInterestPointTriggerEnter != null)
                OnInterestPointTriggerEnter(this);
        }
    }
}

