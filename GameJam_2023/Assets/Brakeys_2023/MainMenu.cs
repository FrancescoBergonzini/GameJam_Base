using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] List<LevelConfig> levels = new List<LevelConfig>();
        [SerializeField] LevelButton levelButtonPrefab;

        [SerializeField] Transform levelsParent;
        [SerializeField] SerializedDictionary<MenuScreen, Transform> screens;

        static bool showMain = true;

        enum MenuScreen
        {
            main,
            levels,
            credits
        }

        Camera _camera;
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            if (showMain)
            {
                MoveToScreen(MenuScreen.main, true);
                showMain= false;
            }
            else
            {
                MoveToScreen(MenuScreen.levels, true);
            }
            foreach (var level in levels)
            {
                var button = Instantiate(levelButtonPrefab, levelsParent);
                button.SetLevel(level);
            }
        }

        void MoveToScreen(MenuScreen screen, bool instant = false, float delay = 0)
        {
            var destScreenPos = screens[screen].position;
            var destination = new Vector3(destScreenPos.x, destScreenPos.y, _camera.transform.position.z);
            if (instant)
            {
                _camera.transform.position = destination;
            }
            else
            {
                _camera.transform.DOMove(destination, 2).SetDelay(delay);
            }
        }

        public void ShowMain()
        {
            MoveToScreen(MenuScreen.main);
        }

        public void ShowLevels()
        {
            MoveToScreen(MenuScreen.levels);
        }

        public void ShowCredits()
        {
            MoveToScreen(MenuScreen.credits);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
