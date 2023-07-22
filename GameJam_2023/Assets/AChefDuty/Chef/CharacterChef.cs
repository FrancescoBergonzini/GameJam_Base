using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore
{
    public class CharacterChef : GameEntity
    {
        public static CharacterChef instance;

        public foodtype current_food_holding = foodtype.none;

        public GameObject current_food_holded;
        public List<GameObject> _food_holding_pref;

        public Dictionary<foodtype, GameObject> food_holding_pref;

        [Header("Raycast logic")]
        public LayerMask foodLayerMask;

        public float raycastDistance = 10f;
        public Transform start_transform;
        public FoodObject food_looking;
        public float check_time = 0.5f;
        public float start_time;
        public float radious;

        [Header("Action logic")]
        KeyCode Action_Key = KeyCode.Mouse0;

        [Header("Trow logic")]
        public Transform trow_point;

        [Space]
        public float trow_force = 5f;

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

        private void Update()
        {
            switch (current_state)
            {

                case state.looking_for_food:

                    Vector3 raycastDirection = start_transform.forward;
                    // Esegui il Raycast
                    RaycastHit hit;

                    if (Physics.Raycast(start_transform.position, raycastDirection, out hit, raycastDistance))
                    {
                        Collider[] colliders = Physics.OverlapSphere(hit.point, radious, foodLayerMask);

                        if (colliders.Length > 0)
                        {
                            // Trovato almeno un oggetto nel raggio della sfera
                            food_looking = colliders[0].GetComponentInParent<FoodObject>();
                        }
                        else
                        {
                            food_looking = null;
                        }
                    }
                    else
                    {
                        food_looking = null;
                    }

                    //start_time = Time.time;
                    //}

                    if (Input.GetKeyDown(Action_Key) && food_looking != null)
                    {
                        current_food_holding = food_looking.ConvertFoodToData();

                        ActiveCorrectFood();

                        PlaySound(AudioType.cath);

                        current_state = state.carring_food;
                    }

                    break;

                case state.carring_food:

                    if (Input.GetKeyDown(Action_Key))
                    {
                        FoodObject.Create((AChefDuty.Instance as AChefDuty).food_configs[current_food_holding], trow_point, trow_force);

                        DeactiveFoods();

                        current_food_holding = foodtype.none;

                        PlaySound(AudioType.trow);

                        current_state = state.looking_for_food;
                    }
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
                    current_food_holded = food;
                    return;
                }
            }
        }

        public void DeactiveFoods()
        {
            foreach (GameObject food in _food_holding_pref)
            {
                if (food.name == current_food_holding.ToString())
                {
                    food.SetActive(false);
                }
            }

            current_food_holded = null;
        }




        private void OnDrawGizmosSelected()
        {
            Vector3 raycastDirection = start_transform.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(start_transform.position, raycastDirection * raycastDistance);

            RaycastHit hit;
            if (Physics.Raycast(start_transform.position, raycastDirection, out hit, raycastDistance))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(hit.point, radious);
            }
        }
    }
}

