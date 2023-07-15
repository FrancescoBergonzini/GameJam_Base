using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
	public interface IParticleDatabase
	{
		(IParticleData data, GameObject instantiated_particle) PlayParticle(System.Enum type, Vector3 position);
	}

	public class ParticleDatabase<E,SE> : MonoBehaviour, IParticleDatabase 
		where E : System.Enum
		where SE : System.Enum // sound enum
	{
		public ParticleData<E, SE>[] Items;


		// indexed dictionary for speed
		private Dictionary<E, ParticleData<E, SE>> _dict = null;


		private void _setup()
		{
			if (_dict != null)
				return;

			_dict = new Dictionary<E, ParticleData<E,SE>>();
			foreach (var i in Items)
			{
				_dict.Add(i.type, i);
			}
		}

		public ParticleData<E, SE> GetItem(E id)
		{
			_setup();
			return _dict[id];			
		}


		public (IParticleData data, GameObject instantiated_particle) PlayParticle(Enum type, Vector3 position)
		{
			var particle = GetItem((E) type);


			if (particle != null && particle.prefab_particle != null)
			{
				//istanzia
				// TODO: instantiate under object?
				GameObject part = Instantiate(particle.prefab_particle, position, Quaternion.identity);

				if (particle.Lifetime > 0)
					Destroy(part, particle.Lifetime);

				return (data: particle, instantiated_particle: part);
			}
			else
			{
				Debug.LogWarning($"ParticleDatabase: no particle fx for type={type}");
				return (data: particle, instantiated_particle: null);
			}
		}

	}

	public interface IParticleData
	{
		System.Enum GetSound();
	}
	
	[Serializable]
	public class ParticleData<E,SE> : IParticleData
		where E : System.Enum
		where SE: System.Enum
	{
		public E type;

		public GameObject prefab_particle;

		[Range(-1f, 10f)]
		public float Lifetime;

		// sound
		public SE sound_type;

		public System.Enum GetSound() => sound_type;
	}
}
