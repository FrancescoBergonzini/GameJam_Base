using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;

namespace GMTK
{
    public class Main : GameSessionBrain
    {
        public EnemyConfig config;
        public override void OnEnter()
        {
            base.OnEnter();

            StartCoroutine(EnemySpawn(config));
        }

        public IEnumerator EnemySpawn(EnemyConfig config)
        {
            while (true)
            {
                yield return new WaitForSeconds(2);

                Enemy.Create(config);
            }
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}

