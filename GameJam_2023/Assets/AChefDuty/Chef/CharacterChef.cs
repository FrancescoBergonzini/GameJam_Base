using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class CharacterChef : MonoBehaviour
    {
        public static CharacterChef instance;

        public FoodObject current_food_holding;

        public List<GameObject> _food_holding_pref;

        public Dictionary<foodtype, GameObject> food_holding_pref;

        public void Awake()
        {
            instance = this;
        }
    }
}

