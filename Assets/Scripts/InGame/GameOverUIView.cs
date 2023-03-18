using DG.Tweening;
using KanKikuchi.AudioManager;
using sabanogames.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGame
{
    public class GameOverUIView : MonoBehaviour
    {
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

        public void Show()
        {
            SEManager.Instance.Play(SEPath.HAIBOKU);
            BGMManager.Instance.Play(BGMPath.GAME_OVER);
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
    }
}
