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

        public foodtype ConvertFoodToData()
        {
            if (this.gameObject != null)
            {
                // Ottieni il tipo di cibo dalla configurazione
                foodtype type = Config.type;

                // Avvia la coroutine per distruggere l'oggetto in modo asincrono
                StartCoroutine(DestroyGameObject());

                return type;
            }
            else
            {
                return foodtype.none;
            }
        }

        private IEnumerator DestroyGameObject()
        {
            // Aspetta un frame prima di distruggere l'oggetto
            yield return null;

            // Distruggi l'oggetto
            Destroy(this.gameObject);
        }
    }
}

