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

        public Color GetColor()
        {
            switch (current_size)
            {
                case Size.SIZE.LITTLE: return small.color;
                case Size.SIZE .MEDIUM: return medium.color;
                case Size.SIZE.LARGE: return large.color;

            }

            return Color.white;
        }

        public Size GetSize()
        {
            switch (current_size)
            {
                case Size.SIZE.LITTLE: return small;
                case Size.SIZE.MEDIUM: return medium;
                case Size.SIZE.LARGE: return large;

            }

            return default;
        }

        public void SwichSize(Size size)
        {

            var sequence = DOTween.Sequence();


            if (current_size == size.state)
            {
                Debug.Log("Sono già di questa dimensione");

                sequence.Join(this.transform.DOShakeScale(ScanlEnigma.Instance.swich_duration, 0.25f));

            }
            else
            {
                sequence.Join(this.transform.DOShakeScale(ScanlEnigma.Instance.swich_duration, 0.25f));
                sequence.Join(this.transform.DOScale(size.dimesion, ScanlEnigma.Instance.swich_duration).SetEase(ScanlEnigma.Instance.swich_ease));
            }

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
        public bool highlighted;

        public bool selected;


        private void OnMouseEnter()
        {
            if (!selected && ScanlEnigma.Instance.CanSelectObjects)
            {
                GetHighlightEffect().glow = 0.5f;
                GetHighlightEffect().SetGlowColor(GetColor());

                highlighted = true;
            }
        }

        private void OnMouseExit()
        {
            if (!selected && ScanlEnigma.Instance.CanSelectObjects)
            {
                GetHighlightEffect().glow = 0.0f;
                highlighted = false;
            }

        }


        private void OnMouseDown()
        {
            if (highlighted && !selected && ScanlEnigma.Instance.CanSelectObjects)
            {
                EnableSelect();
                return;

            }

            if (highlighted && selected)
            {
                DisableSelect();
                return;
            }
        }

        public void DisableSelect(bool absolute = false)
        {
            selected = false;

            GetHighlightEffect().HitFX();
            GetHighlightEffect().glow = absolute ? 0.0f : 0.5f;
            GetHighlightEffect().SetGlowColor(GetColor());

            ScanlEnigma.Instance.OnObjectDeselected(this);
        }

        private void EnableSelect()
        {
            selected = true;

            GetHighlightEffect().HitFX();
            GetHighlightEffect().glow = 1f;
            GetHighlightEffect().SetGlowColor(GetColor());

            ScanlEnigma.Instance.OnObjectSelected(this);

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

