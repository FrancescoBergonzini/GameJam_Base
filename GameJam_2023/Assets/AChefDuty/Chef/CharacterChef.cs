using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class CharacterChef : MonoBehaviour
    {
        public static CharacterChef instance;

        public void Awake()
        {
            instance = this;
        }
    }
}

