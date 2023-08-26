using DG.Tweening;
using GameJamCore.Brakeys_2023;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] RectTransform gameEndPanel;
    [SerializeField] TMP_Text gameEndText;

    [SerializeField] Image[] stars;

    [Space]
    public CanvasGroup[] panels;

    private void Start()
    {
        InitializeUi();

        //GameEnd(10, 3);
    }

    private void InitializeUi()
    {
        foreach (var canvas in panels) canvas.alpha = 0f;

        gameEndPanel.gameObject.SetActive(false);
        gameEndPanel.anchoredPosition = new Vector2(960, 0);

        //imposta il colore delle stelle
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = Color.white;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void SetTimer(float time)
    {
        timerText.text = $"TIMER: " + _formatTime(time);

        string _formatTime(float time)
        {
            int hours = Mathf.FloorToInt(time / 3600);
            int minutes = Mathf.FloorToInt((time % 3600) / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            return string.Format("{0:00}", seconds);
        }
    }

    public void GameEnd(int finalScore, int stars)
    {
        gameEndPanel.gameObject.SetActive(true);
        gameEndPanel.DOAnchorPos(Vector2.zero, 2).SetEase(Ease.OutBack);

        var finalMessage = stars switch
        {
            0 => GetEndMessage("GAME OVER", "TRY AGAIN", "DON'T GIVE UP"),
            1 => GetEndMessage("KEEP GOING", "FIRST STAR", "YOU'RE PROGRESSING"),
            2 => GetEndMessage("WELL DONE", "TWO STARS SHINE", "GROWING STRONG"),
            3 => GetEndMessage("AWESOME", "LEGENDARY", "MASTERFUL"),
        };

        gameEndText.text = $"{finalMessage}!" +
            $"\nYour score: {finalScore}";

        StartCoroutine(ToggleStars(stars, 2));
    }

    string GetEndMessage(params string[] messages)
    {
        return messages[UnityEngine.Random.Range(0, messages.Length)];
    }

    IEnumerator ToggleStars(int starsCount, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        for (int i = 0; i < starsCount; i++)
        {
            Image star = stars[i];
            star.transform.DOPunchScale(Vector3.one * 1.1f, 0.5f);
            star.color = Color.yellow;

            GameManager.instance.PlaySound(GameJamCore.Brakeys_2023.AudioType.start_pop, Vector2.zero);
            yield return new WaitForSeconds(0.5f);

        }
    }
}