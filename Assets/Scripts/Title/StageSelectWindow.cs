
using DG.Tweening;
using sabanogames.Common.UI;
using UniRx;
using UnityEngine;

public class StageSelectWindow : MonoBehaviour
{
    [SerializeField] private CommonButton bgButton;
    [SerializeField] private CanvasGroup bgPanel;
    [SerializeField] private Transform window;
    [SerializeField] private Transform stageSelectItemViewParent;
    [SerializeField] private GameObject stageSelectItemViewPrefab;

    private readonly string[] _createdNameArray = {"rin", "aoto", "gen", "syu", "saba"};
    
    private const float AnimSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        bgButton.OnClickDefendChattering.Subscribe(_ => Hide());
        
        CreateStageView();
    }


    private void CreateStageView()
    {
        for (var i = 0; i < _createdNameArray.Length; i++)
        {
            var obj = GameObject.Instantiate(stageSelectItemViewPrefab, stageSelectItemViewParent);
            var itemView = obj.GetComponent<StageSelectItemView>();
            var stageNo = i + 1;
            itemView.Setup(stageNo,_createdNameArray[i]);
            itemView.gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        DOTween.Sequence()
            .OnStart(() =>
            {
                bgPanel.alpha = 0;
                window.localScale = Vector3.zero;
                bgPanel.gameObject.SetActive(true);
                window.gameObject.SetActive(true);
                gameObject.SetActive(true);
            })
            .Append(bgPanel.DOFade(1, AnimSpeed))
            .Join(window.DOScale(Vector3.one, AnimSpeed));
    }

    public void Hide()
    {
        DOTween.Sequence()
            .Append(bgPanel.DOFade(0, AnimSpeed))
            .Join(window.DOScale(Vector3.zero, AnimSpeed))
            .OnComplete(() =>
            {
                bgPanel.gameObject.SetActive(false);
                window.gameObject.SetActive(false);
                gameObject.SetActive(false);
            });
    }
}
