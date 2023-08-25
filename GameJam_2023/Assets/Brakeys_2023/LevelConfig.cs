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
        public float game_time;


        [Header("Spawn")]
        public Biscotto[] biscotti_to_spawn;
        public float delay_between_each_spawn;


        [Space]
        public float currentScore;
        public bool isUnlocked = false;

        public void SetCurrentScore(float value)
        {
            currentScore = value;
        }
    }
}

