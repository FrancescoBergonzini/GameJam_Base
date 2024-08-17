using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;

namespace ScalEnigma
{
    public class BaseObject : GameEntity
    {
        private Color startcolor;
        void OnMouseEnter()
        {
            startcolor = GetRenderer().material.color;
            GetRenderer().material.color = Color.yellow;
        }
        void OnMouseExit()
        {
            GetRenderer().material.color = startcolor;
        }

        public MeshRenderer rend;
        public MeshRenderer GetRenderer()
        {
            if(rend == null)
                rend = GetComponentInChildren<MeshRenderer>();

            return rend;
        }
    }
}

