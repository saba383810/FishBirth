using DG.Tweening;
using sabanogames.Common.UI;
using UniRx;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleImg;
    [SerializeField] private GameObject tapToStart;
    [SerializeField] private CommonButton startButton;
    [SerializeField] private StageSelectWindow stageSelectWindow;
    private const float AnimSpeed = 3f;

    private void Start()
    {
        ShowTitle();
    }

    private void ShowTitle()
    {
        var pos = titleImg.transform.localPosition;
        var posY = pos.y;
        
        DOTween.Sequence()
            .Append(titleImg.DOFade(1, AnimSpeed))
            .AppendInterval(AnimSpeed / 2)
            .Append(titleImg.transform.DOLocalMoveY(posY + 150, AnimSpeed))
            .OnComplete(SetupTitleButton);
    }

    private void SetupTitleButton()
    {
        startButton.OnClickDefendChattering.Subscribe(_ => ShowStageSelectWindow());
        tapToStart.SetActive(true);
    }

    private void ShowStageSelectWindow()
    {
        stageSelectWindow.Show();
    }
}
