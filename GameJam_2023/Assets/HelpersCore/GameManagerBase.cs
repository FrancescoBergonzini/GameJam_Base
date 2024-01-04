using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static System.Collections.Specialized.BitVector32;

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


    public class GameManagerBase : MonoBehaviour
    {

        // References to sound and particle databases (runtime)
        private ISoundDatabase SoundDatabase;
        private IParticleDatabase ParticleDatabase;


        public static GameManagerBase Instance;


        [Space]
        private List<GameSessionBrain> sections_brains = new List<GameSessionBrain>();
        private GameSessionBrain current_section;

        public GameSessionBrain CurrentSection => current_section;

        //editor debug
        public bool debug_section;


        protected Dictionary<string, GameSessionBrain> sections = new Dictionary<string, GameSessionBrain>();


        private void Awake()
        {
            OnAwake();
        }

        protected virtual void Start()
        {
            //collisions
            SetupCollisionlayers();

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

            //inizialize the section dictionary
            foreach (Transform go in transform)
            {
                var section = go.GetComponent<GameSessionBrain>();
                sections_brains.Add(section);
                sections[section.Name] = section;
            }

            //load the first raw_section as current_section
            ChangeSection();

        }


        #region GameMode

        //default section[0]
        private Coroutine change_section_rountine = null;
        public void ChangeSection(string section_name = null)
        {
            //we are in the same section already?
            if (current_section != null)
            {
                if (section_name == CurrentSection?.Name)
                {
                    Debug.LogError("We already are in this section!");
                    return;
                }
            }

            if (change_section_rountine == null)
                StartCoroutine(_changeSection());

            IEnumerator _changeSection()
            {
                //default
                if (section_name == null)
                    section_name = sections.Keys.FirstOrDefault();

                //call disable?
                var result = current_section?.SetDeactive();

                yield return new WaitForEndOfFrame();

                current_section = sections[section_name];

                yield return new WaitForEndOfFrame();

                result = current_section?.SetActive();

                change_section_rountine = null;
            }

        }


        #endregion

        #region Collision

        public virtual void SetupCollisionlayers()
        {
            //Collision1
            //Character
            //Physics.IgnoreLayerCollision(Layers.Players, Layers.Items, true);
            //Physics.IgnoreLayerCollision(Layers.Players, Layers.Items_parts, true);


            //Collision2
            //Edge


            //Collision3
            //Structure 

            //Collision4
            //Collision5 
            //Collision6
            //Collision7
            //Collision8
            //Collision9
            //Collision10
        }

        #endregion

        #region Particles

        public GameObject PlayParticleMute(System.Enum type, Vector3 position)
        {
            var p = ParticleDatabase.PlayParticle(type, position);

            return p.instantiated_particle;
        }

        public GameObject PlayParticleMute(IParticleData data, Vector3 position, float? z_pos = null)
        {
            if (z_pos != null)
                position.z = (float)z_pos;

            var p = ParticleDatabase.PlayParticle(data, position);

            return p.instantiated_particle;
        }

        public GameObject PlayParticle(System.Enum type, Vector3 position, float? z_pos = null)
        {
            if (z_pos != null)
                position.z = (float)z_pos;

            var p = ParticleDatabase.PlayParticle(type, position);

            System.Enum sound_type = p.data.GetSoundType();


            if ((int)(object)sound_type != 0)
            {
                PlaySound(sound_type, position);
            }

            return p.instantiated_particle;
        }

        public GameObject PlayParticle(IParticleData particle, Vector3 position, float? z_pos = null)
        {
            if (z_pos != null)
                position.z = (float)z_pos;

            var p = ParticleDatabase.PlayParticle(particle, position);

            var sound = p.data.GetSound();

            if (sound != null)
                PlaySound(sound, position);

            return p.instantiated_particle;
        }

        public void PlaySound(System.Enum type, Vector3? position = null, float? volume = null)
        {
            SoundDatabase.Play(type, position, volume);
        }

        public void PlaySound(ISoundData sound, Vector3? position = null, float? volume = null)
        {
            SoundDatabase.Play(sound, position, volume);
        }

        public void PlaySound(AudioClip clip, Vector3? position = null, float? volume = null)
        {
            SoundDatabase.Play(clip, position, volume);
        }


        #endregion

        #region Test

        [ContextMenu("Change to Tutorial section")]
        public void change_to_tutorial()
        {
            ChangeSection("tutorial");
        }

        [ContextMenu("Change to Game Main section")]
        public void change_to_MainGame()
        {
            ChangeSection("main");
        }

        [ContextMenu("Change to Endgame section")]
        public void change_to_EndGame()
        {
            ChangeSection("end");
        }


        //test
        [Space]
        //to use Utilities values use => using static GameJamCore.UnityUtilities;
        public GameJamCore.UnityUtilities.Range range;


        //test
        public GameEntityConfig test_config;

        [ContextMenu("Create TestGameEntity using Config")]
        public void CreateTestGameEntity()
        {
            TestEntity.Create(config: test_config, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
        }

        #endregion


    }
}


