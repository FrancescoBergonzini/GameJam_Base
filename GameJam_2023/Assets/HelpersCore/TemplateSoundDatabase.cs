using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{
    // NOTE: please use integers in blocks 100,200,300 to avoid insertion!
    public enum AudioType
    {
        None = 0,
        notImplemented,

        test_sound = 10,

        bad,
        cath,
        difficulty,
        start,
        good,
        jump,
        trow

            


    }

    public class TemplateSoundDatabase : SoundDatabase<AudioType>
    {
        
    }
}


