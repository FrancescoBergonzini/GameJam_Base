using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamCore
{
    public class AChefDuty : GameManagerBase
    {
        public List<Transform> food_spawn_position;

        [SerializeField] FoodConfig[] _food_config;

        public Dictionary<foodtype, FoodConfig> food_configs;


        public Transform food_holder_transform;

        [Space]
        public int TargetFramrate = 60;

        [Space]
        public int number_of_food_to_get;
        public FoodDeposit food_deposit;

        [Space]
        public int runefy_food;
        public Monolith[] monolith;
        public Sprite[] rune_sprites;


        [Space]
        public float game_time;
        float start_time;
        public TextMeshProUGUI time_text;
        public TextMeshProUGUI game_over_text;
        public GameObject gameover_ui;
        public GameObject gamewin_ui;
        public GameObject tutorial_obj;

        [Space]
        public GameDifficulty current_game_diff;

        public enum GameMode
        {
            none,
            preparation,
            game,
            end
        }

        public GameMode current_mode = GameMode.none;

        public enum game_over_reason
        {
            time_end,
            touch_water,
            recipe_failed,
            object_fall
        }

        public override void OnAwake()
        {
            Application.targetFrameRate = TargetFramrate;

            base.OnAwake();
        }

        private void Start()
        {

            if (current_mode == GameMode.none)
            {
                current_mode = GameMode.preparation;
            }

            food_configs = new Dictionary<foodtype, FoodConfig>();
            //inizialize dictionary
            foreach (FoodConfig food in _food_config)
            {
                food_configs.Add(food.type, food);
            }



        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartScene();
            }

            if(current_mode == GameMode.game)
            {
                time_text.text = DisplayTime(game_time - (Time.time - start_time));

                if(game_time - (Time.time - start_time) <= 0)
                {
                    GameOver(game_over_reason.time_end);
                    
                }
            }
        }


        public void SetUpGame(GameDifficulty difficulties)
        {
            tutorial_obj.SetActive(false);

            current_mode = GameMode.game;

            SpawnFood();

            SetUpFoodDepost(difficulties.food_to_get);

            //
            Runefy(difficulties.runefy);

            game_time = difficulties.time;

            food_deposit.OnAllFoodAdded += GameWin;

            
            start_time = Time.time;
        }

        public void SpawnFood()
        {

            List<int> availablePositions = new List<int>();
            List<int> availableFoods = new List<int>();

            var clone_foods = food_configs.Values.ToList();

            for(int i = 0; i < food_configs.Values.Count; i++)
            {
                int _Position = Random.Range(0, food_spawn_position.Count);
                int _Foods = Random.Range(0, food_spawn_position.Count);

                //presente?
                if (availablePositions.Contains(_Position))
                {
                    i--;
                    continue;
                }

                if (availableFoods.Contains(_Foods))
                {
                    i--;
                    continue;
                }

                
                FoodObject.Create(clone_foods[_Position], food_spawn_position[_Foods]);

                //remove
                availablePositions.Add(_Position);
                availableFoods.Add(_Foods);

            }

            Debug.Log("Complete");

        }

        public void SpawnFood(foodtype[] foods)
        {

        }

        public void SetUpFoodDepost(int foot_to_spaw_number)
        {

            //crea una lista e passala sotto
            List<FoodConfig> _list_to_pass = new List<FoodConfig>();

            List<int> numeri_estratti = new List<int>();

            for(int i = 0; i < foot_to_spaw_number; i++)
            {
                //prendi un numero
                int number = Random.Range(0, food_configs.Keys.Count);

                //il numero è già estratto?
                if (numeri_estratti.Contains(number))
                {
                    //ritorna indietro e rifai
                    i--;
                }
                else
                {
                    _list_to_pass.Add(_food_config[number]);
                    numeri_estratti.Add(number);
                }

            }

            food_deposit.InizializeRequestedResources(_list_to_pass);
        }

        public void Runefy(int number_to_runefy)
        {
            if (number_to_runefy <= 0)
                return;


            List<int> rune_estratte = new List<int>();
            List<int> food_estratti = new List<int>();
            List<int> monolith_estratti = new List<int>();


            //per ogni runa, cambia food image con quello

            for (int i = 0; i < number_to_runefy; i++)
            {
                //prendi una runa random 
                int rune_number = Random.Range(0, rune_sprites.Length);

                //giù presa?
                if (rune_estratte.Contains(rune_number)) { i--; continue; } ;

                //prendi un food random
                int food_number = Random.Range(0, food_deposit.food_icons.Count);

                if (food_estratti.Contains(food_number)) { i--; continue; };

                //estrai monolite
                int monolith_number = Random.Range(0, monolith.Length);
                if (monolith_estratti.Contains(monolith_number)) { i--; continue; };

                //cambia e inizializza monolite valore
                monolith[monolith_number].InizializeMonolith(rune_sprites[rune_number], food_deposit.food_icons[food_number].food_icon.sprite);
                food_deposit.food_icons[food_number].food_icon.sprite = rune_sprites[rune_number];

                //salv risultati non estraibili
                rune_estratte.Add(rune_number);
                food_estratti.Add(food_number);
                monolith_estratti.Add(monolith_number);
                

            }
            //passa a monolith random, food e sprite
        }

        public void GameOver(game_over_reason reason)
        {
            current_mode = GameMode.end;

            PlaySound(GameJamCore.AudioType.bad);
            //Time.timeScale = 0;

            gameover_ui.SetActive(true);

            switch (reason)
            {
                case game_over_reason.time_end: game_over_text.text = "You didn't finish the recipe in time and you died!";
                    break;

                case game_over_reason.touch_water: game_over_text.text = "You drowned by falling into the water!";
                    break;

                case game_over_reason.recipe_failed: game_over_text.text = "You got too many ingredients wrong!";
                    break;
                case game_over_reason.object_fall: game_over_text.text = "A ingredient is fall into the water";
                    break;
            }

            StartCoroutine(_restart());

        }

        public void GameWin()
        {
            current_mode = GameMode.end;
            
            //Time.timeScale = 0;

            CharacterChef.instance.gameObject.SetActive(false);
            gamewin_ui.SetActive(true);

            StartCoroutine(_restart());


        }

        public IEnumerator _restart()
        {

            CharacterChef.instance.gameObject.SetActive(false);
            time_text.gameObject.SetActive(false);

            yield return new WaitForSeconds(6f);

            RestartScene();

            
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        string DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}


