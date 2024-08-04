using GameJamCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class BasePlayer : GameEntity
    {
        public override void InizializeWithConfig(GameEntityConfig config = null)
        {
            OnEntiySpawn.AddListener(OnPlayerSpawn);
            OnEntityDie.AddListener(OnPlayerDie);


            base.InizializeWithConfig(config);

            PlayParticle(ParticleType.test_visual);
        }


        #region Events
        public virtual void OnPlayerSpawn()
        {
            Debug.Log("Player has spawned");
        }

        public virtual void OnPlayerDie()
        {
            Debug.Log("Player has just die");
        }
        #endregion


    }
}


