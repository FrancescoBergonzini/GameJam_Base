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


        private void Start()
        {
            //test
            var _biscotto = BiscottoSpawn();

            _spawned_biscotti.Add(_biscotto);
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

