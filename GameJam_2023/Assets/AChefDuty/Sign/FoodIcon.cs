using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamCore
{
    public class FoodIcon : MonoBehaviour
    {
        public Image back_sprite;
        public Image food_icon;
        public foodtype type;


        public static FoodIcon Create(FoodIcon prefab, foodtype type, Sprite icon, Transform parent)
        {
            FoodIcon food = Instantiate(prefab, parent);

            food.type = type;
            food.food_icon.sprite = icon;

            return food;
        }

        public void CheckChangeBack(foodtype typecheck)
        {
            if (typecheck == type)
            {
                back_sprite.color = Color.green;
            }
            else return;
        }

    }
}

