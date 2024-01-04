using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    [CreateAssetMenu(fileName = "Assets/TestEntityConfig_new.asset", menuName = "GameJamCore/TestEntityConfig")]

    public class TestEntityConfig : GameEntityConfig
    {
        public float life;
        public double damage;
        public new string name;
    }
}

