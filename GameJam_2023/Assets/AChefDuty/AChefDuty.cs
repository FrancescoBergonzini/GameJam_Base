using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class AChefDuty : GameManagerBase
    {
        public Transform[] food_spawn_position;

        [SerializeField] FoodConfig[] _food_config;

        public Dictionary<foodtype, FoodConfig> food_configs;

        private void Start()
        {
            //inizialize dictionary

            foreach(FoodConfig food in _food_config)
            {
                food_configs.Add(food.type, food);
            }
        }
    }
}


