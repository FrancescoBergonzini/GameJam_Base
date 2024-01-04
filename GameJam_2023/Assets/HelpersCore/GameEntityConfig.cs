using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    //[CreateAssetMenu(fileName = "Assets/******_new.asset", menuName = "GameJamCore/****/******")]

    public class GameEntityConfig : ScriptableObject
    {
        public GameEntity prefab;

        public virtual GameEntityConfig Clone()
        {
            var o = Instantiate(this);

            return o;
        }
    }
}

