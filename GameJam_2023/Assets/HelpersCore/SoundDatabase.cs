using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamCore
{
    public interface ISoundDatabase
    {
        void Play(System.Enum type, Vector3? position, float? volume = null);
        void Play(AudioClip clip, Vector3? position, float? volume = null);
        void Play(ISoundData sound, Vector3? position, float? volume = null);
    }

    public class SoundDatabase<E> : MonoBehaviour, ISoundDatabase
        where E : System.Enum
    {
        public SoundData<E>[] Items;

        [Flags]
        public enum Flags
        {
            None = 0,
            CreateAudioSourcesOnAwake = 1 << 0, // will create all audio sources on awake
            LimitSoundsInSameFrame = 1 << 1     // intercepts same sounds playing in the same frame to limit them
        }

        public Flags flags;

        // indexed dictionary for speed
        private Dictionary<E, SoundData<E>> _dict = null;

        void Awake()
        {
            if (flags.HasFlag(Flags.CreateAudioSourcesOnAwake))
            {
                _setup();
                foreach (var s in Items)
                {
                    s.source = gameObject.AddComponent<AudioSource>();
                    s.source.playOnAwake = false;
                }
            }
        }


        private void _setup()
        {
            if (_dict != null)
                return;

            _dict = new Dictionary<E, SoundData<E>>();
            foreach (var i in Items)
            {
                _dict.Add(i.type, i);
            }
        }

        public SoundData<E> GetItem(E id)
        {
            _setup();
            return _dict[id];
        }

        public void Play(Enum type, Vector3? position, float? volume_override = null)
        {
            if (type == null)
                return;

            var s = GetItem((E)type);

            if (s != null)
            {
                Play(s, position, volume_override);
            }
            else
            {
                Debug.LogWarning($"SoundDatabase: no sound fx for type={type}");
                return;
            }
        }

        public void Play(ISoundData s, Vector3? position, float? volume_override = null)
        {
            if (s == null)
                return;

            if (flags.HasFlag(Flags.LimitSoundsInSameFrame))
            {
                // is this sound already played in this frame?
                if (s.Frame == Time.frameCount)
                    return;
            }

            s.Frame = Time.frameCount;

            // create/choose audiosource?
            var source = s.GetSource();

            if (source == null)
            {
                // TODO: GameManager audiosource pool
                s.SetSource(gameObject.AddComponent<AudioSource>());
                source = s.GetSource();
                source.playOnAwake = false;
            }

            source.clip = s.GetClip();    // main clip or randomized from array
                                          //s.source.volume = s.volume;

            s.Play(position: position, volume: volume_override);
        }

        // Very low level overload
        public void Play(AudioClip clip, Vector3? position, float? volume_override = null)
        {
            if (clip == null)
                return;

            // check from existing audiosources if some already has this clip
            var audio_source = (from a in GetComponents<AudioSource>() where a.clip == clip select a).FirstOrDefault();

            if (audio_source == null)
            {
                // TODO: GameManager audiosource pool
                audio_source = gameObject.AddComponent<AudioSource>();
                audio_source.playOnAwake = false;
            }

            audio_source.clip = clip;
            audio_source.PlayOneShot(audio_source.clip, volume_override != null ? (float)volume_override : 1);
        }
    }

    public interface ISoundData
    {
        int Frame { get; set; }     // the frame in which the sound was played last (to check for multiple identical sounds in same frame)

        AudioSource GetSource();
        void SetSource(AudioSource source);
        AudioClip GetClip();

        float Volume { get; }

        void Play(Vector3? position, float? volume = null);
    }

    [Serializable]
    public class SoundData : ISoundData
    {
        [Tooltip("Single clip, if null the clip array is used to randomize the sound")]
        public AudioClip clip;
        [Tooltip("Multiple clips, clip will be chosen randomically if main clip is null")]
        public AudioClip[] clip_array;

        [Range(0, 1)]
        public float volume = 1;
        [Range(0.1f, 3f)]
        public float pitch = 1;

        public bool loop;

        /// <summary>
        /// If true, the sound is play with a different volume and pich every time
        /// </summary>

        [Tooltip("Pitch randomization")]
        public bool randomize = false;
        [Range(0.1f, 0.4f)]
        public float fattore_di_random = 0.2f;

        [HideInInspector]
        public AudioSource source;  //FIXME: use a pool of audiosources ?

        #region ISoundData

        public int Frame { get; set; }

        public AudioSource GetSource() => source;

        public void SetSource(AudioSource _source)
        {
            source = _source;
        }

        public AudioClip GetClip()
        {
            if (clip != null)
            {
                return clip;
            }
            else if (clip_array.Length > 0)
            {
                return TryGet_First_Clip_In_ClipArray();
            }
            else
                return null;

            AudioClip TryGet_First_Clip_In_ClipArray()
            {
                AudioClip clip = null;

                try
                {
                    clip = clip_array[UnityEngine.Random.Range(0, clip_array.Length)];
                }
                catch (Exception e)
                {
                    Debug.LogError("Audio clip mancante, generata " + e.GetType());
                }

                return clip;
            }
        }



        public float Volume => volume;

        public void Play(Vector3? position, float? volume_override = null)
        {
            source.pitch = pitch;
            source.loop = loop;

            if (randomize)
            {
                source.pitch = UnityEngine.Random.Range((1f - fattore_di_random), (1f + fattore_di_random));
            }

            source.PlayOneShot(source.clip, volume_override != null ? (float)volume_override : this.volume);
        }

        #endregion
    }

    [Serializable]
    public class SoundData<E> : SoundData where E : System.Enum
    {
        public E type;
    }
}
