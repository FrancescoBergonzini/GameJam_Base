using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] RectTransform gameEndPanel;
    [SerializeField] TMP_Text gameEndText;

    private void Start()
    {
        gameEndPanel.gameObject.SetActive(false);
        gameEndPanel.anchoredPosition = new Vector2(960,0);
    }

    public void SetScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void GameEnd(int finalScore)
    {
        gameEndPanel.gameObject.SetActive(true);
        gameEndPanel.DOAnchorPos(Vector2.zero, 2).SetEase(Ease.OutBack);
        gameEndText.text = $"GAME OVER!\nYour score: {finalScore}";
    }
}