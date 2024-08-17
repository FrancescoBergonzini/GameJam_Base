using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;

namespace GMTK
{
    public class Edge : MonoBehaviour
    {
        public enum TYPE
        {
            TOP,
            BOTTOM,
            LEFT,
            RIGHT
        }

        public TYPE type;

        public Collider col;
        public Collider GetCollider()
        {
            if(col == null)
                col = GetComponent<Collider>();

            return col;
        }
    }

}
