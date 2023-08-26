using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamCore.Brakeys_2023
{
    public static class Layers
    {
        //new layers test
        public const int Biscotto = 6;
        public const int PezziBiscotto = 7;
        public const int Cucchiaio = 8;
        public const int Tazza = 9;
        public const int Liquido = 10;

        public const int Collision11 = 11;
        public const int Collision12 = 12;
        public const int Collision13 = 13;
        public const int Collision14= 14;
        public const int Collision15 = 15;

        //Others
        public const int IgnoreStructure = 29; //ignora collision Structure & Enemy
        public const int LimitDestroyEdge = 30;
        public const int Structures = 31;
    }


    public class GameManager : GameManagerBase
    {
        public static GameManager instance => Instance as GameManager;

        [Space]
        public LevelConfig[] levels;

        [Space]
        public static LevelConfig current_level = null;
        public float current_raw_score = 0f;

        [Header("Game references")]
        public Transform biscotto_parent;
        public Pinza pinza_prefab;
        public UiManager ui_prefab;


        [Header("Biscotto references")]
        //forse inutile, ma vedremo...
        private List<Biscotto> _spawned_biscotti = new List<Biscotto>();
        [SerializeField] Vector2 biscottoSpawnXRange;
        [SerializeField] float biscottoSpawnY;

        [Header("Game mode")]
        public GameMode current_mode = GameMode.none;
        public string Debug_mode;

        [Space]
        public Tazza current_level_tazza;
        public Pinza current_level_pinza;

        float start_time;

        int biscottoCount = 0;

        public enum GameMode
        {
            none,
            biscotti_spawn,
            cucchiaio_spawn,
            game,
            final_score,
            exit
        }

        

        private void Start()
        {

            //debug
            //TODO: settare current_level dal menu
            if (current_level == null)
                current_level = levels[0];

            StartMainGameRoutine();

            //start biscotti
            OnSpawnBiscottiEnter();
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #region Biscotto Manage

        public Vector2 GetBiscottoSpawnPosition()
        {
            return new Vector2(UnityEngine.Random.Range(biscottoSpawnXRange.x, biscottoSpawnXRange.y), biscottoSpawnY);
        }

        public IEnumerator ManageBiscottiLevelSpawn_cr(LevelConfig levelSpawner, Action OnComplete = null)
        {
            foreach(var biscotto in levelSpawner.biscotti_to_spawn)
            {
                BiscottoSpawn(biscotto);

                yield return new WaitForSeconds(levelSpawner.delay_between_each_spawn);
            }

            biscottoCount = levelSpawner.biscotti_to_spawn.Length;

            OnComplete?.Invoke();
        }


        public Biscotto BiscottoSpawn(Biscotto biscotto_prefab)
        {
            var biscotto = Biscotto.Create(prefab: biscotto_prefab,
                                  position: GetBiscottoSpawnPosition(),
                                  rotation: Quaternion.identity,
                                  force: Vector2.zero,
                                  parent: biscotto_parent);

            _spawned_biscotti.Add(biscotto);

            return biscotto;
        }


        public IEnumerator ManageClawSpawn(Action OnComplete = null)
        {

            yield return new WaitForSeconds(1f);

            GameManager.instance.current_level_pinza = Instantiate(GameManager.instance.pinza_prefab, Vector3.zero, Quaternion.identity);


            OnComplete?.Invoke();
        }
        #endregion

        #region GameMode


        //kill this routine to stop game loop
        public Coroutine MainGameRoutine;

        public void StartMainGameRoutine()
        {
            if (MainGameRoutine == null)
                MainGameRoutine = StartCoroutine(gameLoop_cr());

            IEnumerator gameLoop_cr()
            {
                while (true)
                {
                    switch (current_mode)
                    {
                        case GameMode.none:

                            Debug_mode = "none";
                            break;

                        case GameMode.biscotti_spawn:
                            Debug_mode = "preparazione";
                            break;

                        case GameMode.game:
                            Debug_mode = "game";

                            //
                            ui_prefab.SetScore((int)current_raw_score);

                            ui_prefab.SetTimer(start_time + current_level.game_time - Time.time);

                            //check if game is finish
                            if (start_time + current_level.game_time < Time.time)
                                OnFinalScoreEnter();

                            break;

                        case GameMode.final_score:
                            Debug_mode = "score";
                            break;

                        case GameMode.exit:
                            Debug_mode = "uscita";
                            break;

                    }

                    yield return new WaitForEndOfFrame();
                }
            }
        }


        public void OnSpawnBiscottiEnter()
        {
            //cose che devono accadare la prima volta che si è nello stato preparation

            current_mode = GameMode.biscotti_spawn;

            StartCoroutine(ManageBiscottiLevelSpawn_cr(levelSpawner: current_level, 
                                                       OnComplete: () => OnPinzaSpawnEnter()));
        }

        public void OnPinzaSpawnEnter()
        {
            current_mode = GameMode.cucchiaio_spawn;

            StartCoroutine(ManageClawSpawn(OnComplete: () => OnGameEnter()));
        }

        public void OnGameEnter()
        {
            current_mode = GameMode.game;

            //chiama cose da fare quando Enter inizia
            start_time = Time.time;

            foreach (var canvas in ui_prefab.panels) canvas.alpha = 1f;


        }

        public void OnAddScore(float score)
        {
            biscottoCount--;
            current_raw_score+= score;
            if (biscottoCount <= 0) {
                OnFinalScoreEnter();
            }
        }

        public void OnFinalScoreEnter()
        {
            current_mode = GameMode.final_score;

            foreach (var canvas in ui_prefab.panels) canvas.alpha = 0f;


            current_level_pinza.KillMe();

            //test
            //ui_prefab.GameEnd(500, 2);
            ui_prefab.GameEnd((int)this.current_raw_score, _calculate_stars());
        }

        //da chiamare quando si esce dalla scena!

        public void OnExitEnter()
        {
            current_mode = GameMode.exit;

            //salve punteggio in scriptable
            //solo se è più alto di quello che abbiamo li
            if (current_raw_score > current_level.LevelScore)
                current_level.SetCurrentScoreAndStars(current_raw_score, _calculate_stars());
        }

        //calcola quante stelle devo accendere in base al punteggio...
        int _calculate_stars() //min 0, max 3
        {
            int maxpossibilevalue = 100 * current_level.biscotti_to_spawn.Length;

            var percent = (this.current_raw_score / maxpossibilevalue) * 100;

            if (percent < 25)
                return 0;
            else if (percent >= 25 && percent < 50f)
                return 1;
            else if (percent >= 50 && percent < 70)
                return 2;
            else
                return 3;


        }

        #endregion


    }
}

