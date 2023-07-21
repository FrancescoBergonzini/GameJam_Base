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

        public Transform food_holder_transform;

        [Space]
        public int TargetFramrate = 60;

        [Space]

        public int number_of_food_to_get;
        public FoodDeposit food_deposit;

        public override void OnAwake()
        {
            Application.targetFrameRate = TargetFramrate;

            base.OnAwake();
        }

        private void Start()
        {
            food_configs = new Dictionary<foodtype, FoodConfig>();
            //inizialize dictionary
            foreach(FoodConfig food in _food_config)
            {
                food_configs.Add(food.type, food);
            }


            SpawnFood();

            SetUpFoodDepost();

        }


        public void SpawnFood()
        {

            //numbers
            for (int i = 0; i <= 9; i++)
            {
                availableNumbers.Add(i);
            }

            var clone_foods = food_configs.Values.ToList();

            for(int i = 0; i < food_configs.Values.Count; i++)
            {
                int randomIndex = Random.Range(0, availableNumbers.Count);
                int randomNumber = availableNumbers[randomIndex];

  
                FoodObject.Create(clone_foods[randomNumber], food_spawn_position[randomNumber]);

                //remove
                availableNumbers.Remove(randomNumber);

            }

            Debug.Log("Complete");

        }

        public void SpawnFood(foodtype[] foods)
        {

        }

        public void SetUpFoodDepost()
        {

            //crea una lista e passala sotto
            List<FoodConfig> _list_to_pass = new List<FoodConfig>();

            List<int> numeri_estratti = new List<int>();

            for(int i = 0; i < number_of_food_to_get; i++)
            {
                //prendi un numero
                int number = Random.Range(0, food_configs.Keys.Count);

                //il numero è già estratto?
                if (numeri_estratti.Contains(number))
                {
                    //ritorna indietro e rifai
                    i--;
                }
                else
                {
                    _list_to_pass.Add(_food_config[number]);
                    numeri_estratti.Add(number);
                }

            }

            food_deposit.InizializeRequestedResources(_list_to_pass);
        }
    }
}


