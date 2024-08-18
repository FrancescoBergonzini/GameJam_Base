using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using HighlightPlus;

namespace ScalEnigma
{
    public class BaseObject : GameEntity
    {

        public void Awake()
        {
            GetMouseRedirector().OnMouseOnObject += () => { GetHighlightEffect().highlighted = true; };
            GetMouseRedirector().OnMouseExitFromObject += () => { GetHighlightEffect().highlighted = false; };
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

