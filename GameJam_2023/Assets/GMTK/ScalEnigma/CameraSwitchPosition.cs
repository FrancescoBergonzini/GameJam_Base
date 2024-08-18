using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalEnigma
{
    public class CameraSwitchPosition : MonoBehaviour
    {
        public GameObject cameraa;
        public Transform cameraSwitchPosition;
        public Transform cameraInitialPosition;
        public Ease ease;
        public float time = 2;
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.Player)
            {
                cameraa.transform.DOMove(cameraSwitchPosition.position, 2).SetEase(ease);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == Layers.Player)
            {
                cameraa.transform.DOMove(cameraInitialPosition.position, 2).SetEase(ease);
            }
        }
    }
}

