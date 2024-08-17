using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class GMTK_Player : MonoBehaviour
    {
        public Transform left, right, top, bottom;
        public Ease ease;
        public float lenght;
        public float time;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                top.DOScaleY(lenght, time).From(1).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                Debug.Log("Scale applied");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                bottom.DOScaleY(lenght, time).From(1).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                Debug.Log("Scale applied");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                left.DOScaleX(lenght, time).From(1).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                Debug.Log("Scale applied");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                right.DOScaleX(lenght, time).From(1).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                Debug.Log("Scale applied");
            }
        }
    }
}

