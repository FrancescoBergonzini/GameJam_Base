using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class TestEntity : GameEntity
    {
        public new TestEntityConfig Config => (TestEntityConfig)base.Config;
        public new TestEntityConfig Runtime => (TestEntityConfig)base.Runtime;

        public override void InizializeWithConfig(GameEntityConfig config = null)
        {
            base.InizializeWithConfig(config);

            Debug.Log($"Si è inizializzato: {Runtime.name},{nameof(Runtime.life)}:{Runtime.life},{nameof(Runtime.damage)}:{Runtime.damage}");
        }

    }

}
