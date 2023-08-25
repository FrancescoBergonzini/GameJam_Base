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
        public float LevelScore = 0;
        public int Stars = 0;
        public bool isUnlocked = false;

        [Space]
        public LevelConfig next_level;

        public void SetCurrentScoreAndStars(float score, int stars)
        {
            LevelScore = score;
            Stars = stars;

            //unlock here next level?
            if (stars > 0 && next_level != null)
                next_level.isUnlocked = true;
        }
    }
}

