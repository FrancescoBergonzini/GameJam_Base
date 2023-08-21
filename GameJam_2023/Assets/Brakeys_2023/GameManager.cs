using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class GameManager : GameManagerBase
    {

        [Header("Game references")]
        public Transform biscotto_parent;


        [Header("Biscotto references")]
        public Biscotto biscotto_prefab;
        //forse inutile, ma vedremo...
        private List<Biscotto> _spawned_biscotti = new List<Biscotto>();


        [Header("Game mode")]
        public GameMode current_mode = GameMode.none;
        public string Debug_mode;
        public enum GameMode
        {
            none,
            preparation,
            game,
            final_score,
            exit
        }

        

        private void Start()
        {
            StartMainGameRoutine();

            //passo da none a preparation
            OnPreparationEnter();
        }



        #region Biscotto Manage

        public Vector2 GetBiscottoSpawnPosition()
        {
            return Vector2.zero;
        }


        public Biscotto BiscottoSpawn()
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

                        case GameMode.preparation:
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


        public void OnPreparationEnter()
        {
            //cose che devono accadare la prima volta che si è nello stato preparation

            current_mode = GameMode.preparation;
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
        }

        public void OnExitEnter()
        {
            current_mode = GameMode.exit;
        }


        #endregion

    }
}

