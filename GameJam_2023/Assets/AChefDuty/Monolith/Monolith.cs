using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamCore
{
    public class Monolith : MonoBehaviour
    {
        public Image rune_image;
        public Image food_image;
        public Transform panel;


        public void InizializeMonolith(Sprite rune, Sprite food)
        {
            panel.gameObject.SetActive(true);

            rune_image.sprite = rune;
            food_image.sprite = food;
        }
    }
}

