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
        // Start is called before the first frame update
        void Start()
        {
            foreach (var level in levels)
            {
                var button = Instantiate(levelButtonPrefab, levelsParent);
                button.SetLevel(level);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
