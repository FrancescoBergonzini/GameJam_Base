using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class AchivementSystem : MonoBehaviour
    {
        private void Awake()
        {
            PlayerPrefs.DeleteAll();

            InterestPoint.OnInterestPointTriggerEnter += InterestPoint_OnInterestPoinTriggerEnter;
        }

        void InterestPoint_OnInterestPoinTriggerEnter(InterestPoint ip)
        {
            string achivementKey = "achivement" + ip.Name;

            if (PlayerPrefs.GetInt(achivementKey) == 1)
                return;

            PlayerPrefs.SetInt(achivementKey, 1);

            Debug.Log("Unlock" + ip.Name);
        }
    }
}

