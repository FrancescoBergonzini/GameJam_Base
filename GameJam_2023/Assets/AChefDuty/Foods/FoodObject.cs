using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

namespace GameJamCore
{

    public class FoodObject : MonoBehaviour
    {

        public FoodConfig Config;

        public Rigidbody RB;

        public static void Create(FoodConfig config, Transform pos, float trow_force = 0)
        {
            FoodObject food = null;

            if((AChefDuty.Instance as AChefDuty).current_mode == AChefDuty.GameMode.preparation)
            {
                food = Instantiate(config.prefab, pos.position, Quaternion.identity, (AChefDuty.Instance as AChefDuty).tutorial_obj.transform);
            }
            else
            {
                food = Instantiate(config.prefab, pos.position, Quaternion.identity, (AChefDuty.Instance as AChefDuty).food_holder_transform);
            }

            
            food.Config = config;
            food.RB = food.GetComponent<Rigidbody>();

            if (trow_force != 0) food.RB.AddForce(pos.forward * trow_force, ForceMode.Impulse);


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

