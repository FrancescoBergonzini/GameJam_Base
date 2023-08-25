using DG.Tweening;
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
        gameEndText.text = $"GAME OVER!\nYour score: {finalScore}";

        StartCoroutine(ToggleStars(stars, 2));
    }

    IEnumerator ToggleStars(int starsCount, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        for (int i = 0; i < starsCount; i++)
        {
            Image star = stars[i];
            star.transform.DOPunchScale(Vector3.one * 1.1f, 0.5f);
            star.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
        }
    }
}