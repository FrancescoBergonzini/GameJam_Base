using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
	public interface ISoundDatabase
	{
		void Play(System.Enum type, Vector3? position);
	}

	public class SoundDatabase<E> : MonoBehaviour, ISoundDatabase
		where E : System.Enum
	{
		public SoundData<E>[] Items;


		// indexed dictionary for speed
		private Dictionary<E, SoundData<E>> _dict = null;


		private void _setup()
		{

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

		public void Play(Enum type, Vector3? position)
		{
			if (type == null)
				return;

			var s = GetItem((E) type);

			if (s != null)
			{
				// create/choose audiosource?
				if (s.source == null)
				{
					// TODO: GameManager audiosource pool
					s.source = gameObject.AddComponent<AudioSource>();
					s.source.playOnAwake = false;
					s.source.clip = s.clip;
					//s.source.volume = s.volume;
					s.source.pitch = s.pitch;
					s.source.loop = s.loop;
				}


				if (s.randomize)
				{
					s.source.pitch = UnityEngine.Random.Range((1 - s.fattore_di_random), (1 + s.fattore_di_random));
				}

				//s.source.Play();
				s.source.PlayOneShot(s.source.clip, s.volume);

			}
			else
			{
				Debug.LogWarning($"SoundDatabase: no sound fx for type={type}");
				return;
			}
		}		
	}

	
	[Serializable]
	public class SoundData<E> where E : System.Enum
	{
		public E type;

		public AudioClip clip;
		[Range(0, 1)]
		public float volume;
		[Range(0.1f, 3f)]
		public float pitch;

		public bool loop;

		/// <summary>
		/// If true, the sound is play with a different volume and pich every time
		/// </summary>
		public bool randomize = false;
		[Range(0.1f, 0.4f)]
		public float fattore_di_random = 0.2f;

		[HideInInspector]
		public AudioSource source;	//FIXME: use a pool of audiosources ?
	}
}
