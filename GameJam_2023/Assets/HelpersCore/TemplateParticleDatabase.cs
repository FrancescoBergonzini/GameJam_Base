using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    // NOTE: please use integers in blocks 100,200,300 to avoid insertion!
    public enum ParticleType
    {
        None = 0,
        notImplemented,

        test_visual = 10


    }

    public class TemplateParticleDatabase : ParticleDatabase<ParticleType, AudioType>
    {

    }
}


