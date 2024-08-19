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
        [Space]
        public bool CanChangeSize = true;

        [Space]
        public SoundData Select_sfx;
        public SoundData Deselect_sfx;

        public void Awake()
        {
            SetLayerRecursively(this.gameObject, Layers.Object);
        }

        [Serializable]
        public struct Size
        {
            public enum STATE
            {
                SMALL,
                MEDIUM,
                LARGE
            }
            public STATE size;


            public Vector3 dimesion;

            public float z_pos;

            public Color color;
        }

        public Size.STATE current_size = Size.STATE.MEDIUM;

        [Space]
        public Size large;
        public Size medium;
        public Size small;

        public Color GetColor()
        {
            switch (current_size)
            {
                case Size.STATE.SMALL: return small.color;
                case Size.STATE .MEDIUM: return medium.color;
                case Size.STATE.LARGE: return large.color;

            }

            return Color.white;
        }

        public Size GetSize()
        {
            switch (current_size)
            {
                case Size.STATE.SMALL: return small;
                case Size.STATE.MEDIUM: return medium;
                case Size.STATE.LARGE: return large;

            }

            return default;
        }

        public Size GetSize(Size.STATE size)
        {
            switch (size)
            {
                case Size.STATE.SMALL: return small;
                case Size.STATE.MEDIUM: return medium;
                case Size.STATE.LARGE: return large;

            }

            return default;
        }



        public void SwichSize(Size size)
        {
            var sequence = DOTween.Sequence();


            if (current_size == size.size)
            {
                Debug.Log("Sono già di questa dimensione");

                sequence.Join(this.transform.DOShakeScale(ScanlEnigma.Instance.swich_duration, 0.25f));

                //PlaySound(SameSize_sfx);

            }
            else
            {
                if(current_size == Size.STATE.LARGE || current_size == Size.STATE.MEDIUM && size.size == Size.STATE.LARGE)
                {
                    //PlaySound(Minimize_sfx);
                }
                
                if(current_size == Size.STATE.SMALL || current_size == Size.STATE.MEDIUM && size.size == Size.STATE.SMALL)
                {
                    //PlaySound(Maximize_sfx);

                }

                sequence.Join(this.transform.DOShakeScale(ScanlEnigma.Instance.swich_duration, 0.25f));
                sequence.Join(this.transform.DOScale(size.dimesion, ScanlEnigma.Instance.swich_duration).SetEase(ScanlEnigma.Instance.swich_ease));
                sequence.Join(this.transform.DOMoveZ(size.z_pos, ScanlEnigma.Instance.swich_duration));

            }

            current_size = size.size;



        }

        public void SwichSize(Size.STATE state)
        {
            SwichSize(GetSize(state));
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
            if (!selected && ScanlEnigma.Instance.CanSelectObjects && CanChangeSize)
            {
                GetHighlightEffect().glow = 1f;
                GetHighlightEffect().SetGlowColor(GetColor());

                highlighted = true;

            }
        }

        private void OnMouseExit()
        {
            if (!selected && ScanlEnigma.Instance.CanSelectObjects && CanChangeSize)
            {
                GetHighlightEffect().glow = 0.0f;
                highlighted = false;
            }

        }


        private void OnMouseDown()
        {
            if (highlighted && !selected && ScanlEnigma.Instance.CanSelectObjects && CanChangeSize)
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
            GetHighlightEffect().glow = absolute ? 0.0f : 1f;
            GetHighlightEffect().SetGlowColor(GetColor());

            ScanlEnigma.Instance.OnObjectDeselected(this);

            PlaySound(Deselect_sfx);

            this.transform.DOShakeScale(0.5f, 0.1f);


        }

        private void EnableSelect()
        {
            selected = true;

            GetHighlightEffect().HitFX();
            GetHighlightEffect().glow = 2f;
            GetHighlightEffect().SetGlowColor(GetColor());

            ScanlEnigma.Instance.OnObjectSelected(this);

            PlaySound(Select_sfx);

            this.transform.DOShakeScale(0.5f, 0.1f);


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

