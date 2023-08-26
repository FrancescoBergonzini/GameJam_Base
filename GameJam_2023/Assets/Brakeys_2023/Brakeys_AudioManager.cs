using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public enum AudioType
    {
        None = 0,
        notImplemented,

        test_sound = 10,

        //
        splash_biscuits = 20,
        active_claw = 21,
        closing_claw = 22,
        biscuit_to_points = 23,

        //ui
        buttons_menu_0 = 30,
        button_menu_1,
        button_menu_3,
        start_pop = 40,

        //game 
        start_game = 60, //fischietto
        end_game

    }

    public class Brakeys_AudioManager : SoundDatabase<AudioType>
    {

    }
}


