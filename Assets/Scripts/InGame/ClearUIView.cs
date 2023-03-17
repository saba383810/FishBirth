using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using sabanogames.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUIView : MonoBehaviour
{ 
    [Header("ScoreView")]
    [SerializeField] private Text scoreText;

    [SerializeField] private Transform window;
    [SerializeField] private CanvasGroup bgPanel;

    [SerializeField] private CommonButton retryButton;
    [SerializeField] private CommonButton titleButton;
     
    private const float AnimSpeed = 0.3f;

    private void Start()
    {
        retryButton.OnClickOnce.TakeUntilDestroy(gameObject).Subscribe(_ => Retry());
        titleButton.OnClickOnce.TakeUntilDestroy(gameObject).Subscribe(_ => ChangeTitleScene());
    }

    public void Show(int score)
    {
        SetScoreView(score);
        DOTween.Sequence()
            .OnStart(() =>
            {
                window.localScale = Vector3.zero;
                bgPanel.alpha = 0;
                window.gameObject.SetActive(true);
                bgPanel.gameObject.SetActive(true);
                gameObject.SetActive(true);
            })
            .Append(window.DOScale(Vector3.one, AnimSpeed))
            .Join(bgPanel.DOFade(1, AnimSpeed));
    }


    public void Retry()
    {
        // 現在のシーンを取得
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        SceneManager.LoadScene(buildIndex);
    }

    public void ChangeTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    
    /// <summary>
    ///    ScoreのUITextの値を変更する
    /// </summary>
    public void SetScoreView(int score)
    {
        // ScoreTextの変更
        scoreText.text = score.ToString("000000");
    }
}
