using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class CharacterChef : MonoBehaviour
    {
        public static CharacterChef instance;

        public foodtype current_food_holding = foodtype.none;

        public List<GameObject> _food_holding_pref;

        public Dictionary<foodtype, GameObject> food_holding_pref;

        [Header("Raycast logic")]
        public LayerMask objectsLayerMask;
        public float raycastDistance = 10f;
        public Transform start_transform;
        public FoodObject food_looking;
        public float check_time = 0.5f;
        public float start_time;

        [Header("Action logic")]
        KeyCode Action_Key = KeyCode.Mouse0;

        public enum state
        {
            none,
            looking_for_food,
            carring_food
        }

        public state current_state = state.none;

        public void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            current_state = state.looking_for_food;

            start_time = Time.time;
        }

        private void FixedUpdate()
        {
            switch (current_state)
            {

                case state.looking_for_food:
                    // Ottieni la direzione davanti alla trasformazione

                    if (Time.time > start_time + check_time)
                    {
                        Vector3 raycastDirection = start_transform.forward;
                        // Esegui il Raycast
                        RaycastHit hit;

                        if (Physics.Raycast(start_transform.position, raycastDirection, out hit, raycastDistance, objectsLayerMask))
                        {
                            // Un oggetto è stato colpito dal Raycast nel layer "oggetti"
                            food_looking = hit.collider.gameObject.GetComponentInParent<FoodObject>();
                        }
                        else food_looking = null;

                        start_time = Time.time;
                    }

                    if (Input.GetKeyDown(Action_Key) && food_looking != null)
                    {
                        current_food_holding = food_looking.ConvertFoodToData();

                        ActiveCorrectFood();

                        current_state = state.carring_food;
                    }

                    break;

                case state.carring_food:

                    break;
            }

        }

        public void ActiveCorrectFood()
        {
            //
            foreach(GameObject food in _food_holding_pref)
            {
                if(food.name == current_food_holding.ToString())
                {
                    food.SetActive(true);
                    return;
                }
            }
        }

        public void TrowFood()
        {
            //genera oggetto e lancialo

            //distruggi e ripulisci me
        }


        private void OnDrawGizmos()
        {
            Vector3 raycastDirection = start_transform.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(start_transform.position, raycastDirection * raycastDistance);

            RaycastHit hit;
            if (Physics.Raycast(start_transform.position, raycastDirection, out hit, raycastDistance, objectsLayerMask))
            {
                // Se il Raycast colpisce un oggetto nel layer "oggetti", mostra il punto di impatto come Gizmo
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 1f);
            }
        }
    }
}

