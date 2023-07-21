using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace GameJamCore
{

    public class FoodObject : MonoBehaviour
    {

        public FoodConfig Config;

        public static void Create(FoodConfig config, Vector3 pos)
        {
            FoodObject food = Instantiate(config.prefab, pos, Quaternion.identity);
            food.Config = config;
        }

        private void Start()
        {
            
        }
    }
}

