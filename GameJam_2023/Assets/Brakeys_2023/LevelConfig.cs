using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Brakeys_2023/LevelConfig", order = 1)]
    public class LevelConfig : ScriptableObject
    {

        [Space]
        public string level_name;
        public int level;


        [Header("Spawn")]
        public Biscotto[] biscotti_to_spawn;
        public float delay_between_each_spawn;

    }
}

