using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Biscotto : GameEntity
    {
        Rigidbody2D _rdb;
        Collider _col;

        public static Biscotto Create(Biscotto prefab, Vector3 position, Quaternion rotation, Vector2 force, Transform parent)
        {
            Biscotto _biscotto = Instantiate(prefab, position, rotation, parent);

            _biscotto.Inizialize();

            return _biscotto;
        }


        public void Inizialize()
        {
            if (_rdb == null) 
                _rdb = GetComponent<Rigidbody2D>();

            if (_col == null)
                _col = GetComponent<Collider>();
        }
    }
}

