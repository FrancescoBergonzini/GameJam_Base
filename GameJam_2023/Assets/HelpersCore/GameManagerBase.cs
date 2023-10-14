using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamCore
{
    public static class Layers
    {
        //new layers test
        public const int Layer0 = 10;
        public const int Layer1 = 11;
        public const int Layer2 = 12;
        public const int Layer3 = 13;
        public const int Layer4 = 14;
        public const int Layer5 = 15;

        public const int Layer6 = 16;
        public const int Layer7 = 17;
        public const int Layer8 = 18;
        public const int Layer9 = 19;
        public const int Layer10 = 20;

    }

    public enum GameMode
    {
        none,
        gamemode_0,
        gamemode_1,
        gamemode_2,
    }

    public class GameManagerBase : MonoBehaviour
    {

        // References to sound and particle databases (runtime)
        private ISoundDatabase SoundDatabase;
        private IParticleDatabase ParticleDatabase;

        public static GameManagerBase Instance;

        [Header("Game mode")]
        private GameMode current_mode = GameMode.none;

        public string Debug_mode;

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            StartMainGameRoutine();
        }

        public virtual void OnAwake()
        {
            if (!this.enabled)
                return;

            Instance = this;

            SoundDatabase = GetComponent<ISoundDatabase>();
            if (SoundDatabase == null)
            {
                Debug.LogError("GameManagerBase needs a SoundDatabase");
            }

            ParticleDatabase = GetComponent<IParticleDatabase>();
            if (ParticleDatabase == null)
            {
                Debug.LogError("GameManagerBase needs a ParticleDatabase");
            }
        }

        #region Particles

        public GameObject PlayParticleMute(System.Enum type, Vector3 position)
        {
            var p = ParticleDatabase.PlayParticle(type, position);

            return p.instantiated_particle;
        }

        public GameObject PlayParticle(System.Enum type, Vector3 position)
        {
            var p = ParticleDatabase.PlayParticle(type, position);
            System.Enum sound_type = p.data.GetSound();

            if ((int)(object)sound_type != 0)
            {
                PlaySound(sound_type, position);
            }

            return p.instantiated_particle;
        }

        public void PlaySound(System.Enum type, Vector3? position)
        {
            SoundDatabase.Play(type, position);
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

                        case GameMode.gamemode_0:
                            Debug_mode = "gamemode_0";
                            break;

                        case GameMode.gamemode_1:
                            Debug_mode = "gamemode_1";
                            break;

                        case GameMode.gamemode_2:
                            Debug_mode = "gamemode_2";
                            break;

                    }

                    yield return new WaitForEndOfFrame();
                }
            }
        }

        //Todo: da riguardare, qua usi dei motodi per entrare in uno stato e metti in coda la logica onEnter
        //potresti provare una macchina a stati come swich di behaviour del game main...
        //andrebbe tutto fatto in overload per permettere al game derivati di adottare quello che vogliono

        public void OnGameModeEnter0()
        {
            //cose che devono accadare la prima volta che si � nello stato preparation

            current_mode = GameMode.gamemode_0;

        }

        public void OnGameModeEnter1()
        {
            //cose che devono accadare la prima volta che si � nello stato preparation

            current_mode = GameMode.gamemode_1;

        }

        public void OnGameModeEnter2()
        {
            //cose che devono accadare la prima volta che si � nello stato preparation

            current_mode = GameMode.gamemode_2;

        }



        #endregion
    }
}


