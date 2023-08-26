using GameJamCore.Brakeys_2023;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] TMP_Text levelName;
    [SerializeField] Button button;
    [SerializeField] GameObject[] stars;

    internal void SetLevel(LevelConfig level)
    {
        levelName.text = level.level.ToString();
        button.interactable = level.isUnlocked;
        if (level.isUnlocked)
        {
            button.onClick.AddListener(() =>
            {
                GameManager.current_level = level;
                SceneManager.LoadScene("Brakeys_2023");
            });
        }
        for (int i = 0; i < level.Stars; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
