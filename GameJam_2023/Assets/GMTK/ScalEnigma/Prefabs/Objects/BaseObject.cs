using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using HighlightPlus;
using DG.Tweening;

namespace ScalEnigma
{
    public class BaseObject : GameEntity
    {
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
        }

        public Size current_size = default;

        

        public void SwichSize(Size size)
        {
            if (current_size.state == size.state)
            {
                Debug.Log("Sono già di questa dimensione");
            }

            current_size = size;

            //this.transform.DOScale
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

        private OnMouseRedirector mouse_redirector;
        public OnMouseRedirector GetMouseRedirector()
        {
            if(mouse_redirector == null)
            {
                mouse_redirector = GetComponentInChildren<OnMouseRedirector>();
            }

            return mouse_redirector;

        }

        public MeshRenderer rend;
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

