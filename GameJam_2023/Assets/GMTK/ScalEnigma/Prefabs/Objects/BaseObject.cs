using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using HighlightPlus;
using DG.Tweening;
using System;

namespace ScalEnigma
{
    public class BaseObject : GameEntity
    {
        [Serializable]
        public struct Size
        {
            public enum SIZE
            {
                LITTLE,
                MEDIUM,
                LARGE
            }
            public SIZE state;


            public Vector3 dimesion;

            public Color color;
        }

        public Size.SIZE current_size = Size.SIZE.MEDIUM;

        [Space]
        public Size large;
        public Size medium;
        public Size small;

        public void SwichSize(Size size)
        {
            if (current_size == size.state)
            {
                Debug.Log("Sono già di questa dimensione");
            }

            var sequence = DOTween.Sequence();

            sequence.Append(this.transform.DOScale(size.dimesion, ScanlEnigma.Instance.swich_duration).SetEase(ScanlEnigma.Instance.swich_ease));
            
            current_size = size.state;

        }

        #region Testing

        [ContextMenu("Swich large")]
        public void SwichLarge()
        {
            SwichSize(large);
        }

        [ContextMenu("Swich medium")]
        public void SwichMedium()
        {
            SwichSize(medium);
        }

        [ContextMenu("Swich small")]
        public void SwichSmall()
        {
            SwichSize(small);
        }

        #endregion

        [Space]
        public bool selected;

        public bool Selected
        {
            get { return selected; }
            set { 
                
                if (value)
                {

                }
                else
                {

                }
            
            }
        }
        private void OnMouseEnter()
        {
            { GetHighlightEffect().highlighted = true; };
        }


        private void OnMouseExit()
        {
            { GetHighlightEffect().highlighted = false; };
        }


        #region Helpers

        private MeshRenderer rend;
        public MeshRenderer GetRenderer()
        {
            if(rend == null)
                rend = GetComponentInChildren<MeshRenderer>();

            return rend;
        }

        HighlightEffect highlight;

        public HighlightEffect GetHighlightEffect()
        {
            if (highlight == null)
            {
                highlight = GetComponent<HighlightEffect>();
            }

            return highlight;
        }

        #endregion
    }
}

