using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    public class DeathCube : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                (AChefDuty.Instance as AChefDuty).GameOver(AChefDuty.game_over_reason.touch_water);
            }
        }
    }
}

