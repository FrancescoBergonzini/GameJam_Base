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

        private void Start()
        {
            //test
            BiscottoSpawn();
        }


        #region Biscotto Manage

        public Vector2 GetBiscottoSpawnPosition()
        {
            return Vector2.zero;
        }


        public Biscotto BiscottoSpawn()
        {
            return Biscotto.Create(prefab: biscotto_prefab,
                                  position: Vector3.zero,
                                  rotation: Quaternion.identity,
                                  force: GetBiscottoSpawnPosition(),
                                  parent: biscotto_parent);
        }

        #endregion

    }
}
