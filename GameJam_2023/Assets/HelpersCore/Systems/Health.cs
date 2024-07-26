using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    //Usa [RequireAttribute(typeof(HealthSystem)) nelle classi che utilizzano questo system
    public class Health : MonoBehaviour
    {
        public Action OnDeath;

        [SerializeField] int health;
        [SerializeField] int maxHealth;

        public void Inizialize()
        {
            health = maxHealth;
        }

        public void OnDamageDealt(int damage)
        {
            health -= damage;

            if(health < 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}

