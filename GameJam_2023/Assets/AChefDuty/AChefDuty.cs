using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamCore
{
    public class AChefDuty : GameManagerBase
    {
        public List<Transform> food_spawn_position;

        [SerializeField] FoodConfig[] _food_config;

        public Dictionary<foodtype, FoodConfig> food_configs;

        public List<int> availableNumbers = new List<int>();


    private void Start()
        {
            food_configs = new Dictionary<foodtype, FoodConfig>();
            //inizialize dictionary
            foreach(FoodConfig food in _food_config)
            {
                food_configs.Add(food.type, food);
            }

            //numbers
            for (int i = 0; i <= 9; i++)
            {
                availableNumbers.Add(i);
            }

            SpawnFood();
        }


        public void SpawnFood()
        {
            var clone_foods = food_configs.Values.ToList();

            for(int i = 0; i < food_configs.Values.Count; i++)
            {
                int randomIndex = Random.Range(0, availableNumbers.Count);
                int randomNumber = availableNumbers[randomIndex];

  
                FoodObject.Create(clone_foods[randomNumber], food_spawn_position[randomNumber].position);

                //remove
                availableNumbers.Remove(randomNumber);

            }

            Debug.Log("Complete");

        }

        public void SpawnFood(foodtype[] foods)
        {

        }
    }
}


