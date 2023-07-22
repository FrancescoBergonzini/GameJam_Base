using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamCore
{
    public class FoodDeposit : MonoBehaviour
    {
        public static FoodDeposit instance;

        public List<FoodConfig> request_food_to_deposit;

        public Action<foodtype> OnFoodAdded;
        public Action OnAllFoodAdded;

        [Header("Sign")]
        public FoodIcon icon_food_prefab;
        public Transform icon_parent;


        private void Start()
        {
            instance = this;
        }

        public void InizializeRequestedResources(List<FoodConfig> requested)
        {
            request_food_to_deposit = requested.ToList();

            //add icons
            foreach(FoodConfig food in request_food_to_deposit)
            {
                var token = FoodIcon.Create(icon_food_prefab, food.type, food.icon, icon_parent) ;

                OnFoodAdded += token.CheckChangeBack;

            }


        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == 7)
            {
                var food = collision.gameObject.GetComponent<FoodObject>();

                if (request_food_to_deposit.Contains(food.Config))
                {
                    food.ConvertFoodToData();
                    request_food_to_deposit.Remove(food.Config);

                    OnFoodAdded?.Invoke(food.Config.type);
                }

                if(request_food_to_deposit.Count == 0)
                {
                    OnAllFoodAdded?.Invoke();
                }
            }
        }

        public void CheckResource(foodtype food_to_check)
        {

        }

    }
}


