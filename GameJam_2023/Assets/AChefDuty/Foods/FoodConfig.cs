using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public enum foodtype
    {
        fish,
        apple,
        watermelon,
        turkey,
        coconut,
        whileHam,
        peneapple,
        pepper,
        meatRaw,
        mushroom
    }

    [CreateAssetMenu(fileName = "Assets/FoodConfig_new.asset", menuName = "GameJam/AChefDuty/FoodConfigConfig")]
    public class FoodConfig : ScriptableObject
    {
        public foodtype type;
        public Sprite icon;
        public FoodObject prefab;

    }
}
