using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameJamCore
{
    [Serializable]
    public struct GameDifficulty
    {
        public string name;
        public int food_to_get;
        public int runefy;
        public float time;
    }

    public class DifficultyChanger : GameEntity
    {

        [Space]
        public TextMeshPro difficulty_text;
        public List<GameDifficulty> difficulties;
        public int difficulty_counter = 0;

        private void Start()
        {
            (AChefDuty.Instance as AChefDuty).current_game_diff = difficulties[difficulty_counter];
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 7)
            {
                PlaySound(AudioType.difficulty);
                //chenge difficulties
                difficulty_counter++;
                if (difficulty_counter == 3) difficulty_counter = 0;

                (AChefDuty.Instance as AChefDuty).current_game_diff = difficulties[difficulty_counter];
                difficulty_text.text = difficulties[difficulty_counter].name;

            }
        }
    }
}

