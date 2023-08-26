using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public enum ParticleType
    {
        None = 0,
        notImplemented,

        test_visual = 10,
        liquid = 11,
        stars
    }

    public class Brakeys_ParticleManager : ParticleDatabase<ParticleType, AudioType>
    {

    }
}

