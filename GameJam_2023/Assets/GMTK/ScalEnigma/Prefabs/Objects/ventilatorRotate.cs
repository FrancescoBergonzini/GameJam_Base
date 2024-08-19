using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalEnigma
{
    public class ventilatorRotate : MonoBehaviour
    {
        public float time_for_fullRotation = 3;
        void Update()
        {
            this.transform.DORotate(new Vector3(0, 360, 0), time_for_fullRotation, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        }
    }

}
