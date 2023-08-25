using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public LevelConfig current_level = null;

        [Header("Game references")]
        public Transform biscotto_parent;


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
        public Cucchiaio current_level_cucchiaio;

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

            OnSpawnBiscottiEnter();
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

            OnComplete.Invoke();
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
                                                       OnComplete: () => OnCucchiaioSpawnEnter()));
        }

        public void OnCucchiaioSpawnEnter()
        {
            current_mode = GameMode.cucchiaio_spawn;
        }
        public void OnGameEnter()
        {
            //chiama le cose da fare quando preparatione finisce

            //chiama cose da fare quando Enter inizia

            current_mode = GameMode.game;
        }


        public void OnFinalScoreEnter()
        {
            current_mode = GameMode.final_score;

            //TODO: settare punteggio
            //current_level.SetCurrentScore(score);
        }

        public void OnExitEnter()
        {
            current_mode = GameMode.exit;
        }


        #endregion

    }
}

