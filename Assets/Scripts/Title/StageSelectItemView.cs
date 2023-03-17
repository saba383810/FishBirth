using KanKikuchi.AudioManager;
using sabanogames.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectItemView : MonoBehaviour
{
    [SerializeField] private Text stageNameText;
    [SerializeField] private Text createdByNameText;
    [SerializeField] private CommonButton stageButton;
 
    private int _stageNo;
    private string _createdByName;

    public void Setup(int stageNo,string createdByName)
    {
        _stageNo = stageNo;
        _createdByName = createdByName;
        createdByNameText.text = $"Created by\n{_createdByName}";
        stageNameText.text = $"Stage{_stageNo}";
        stageButton.OnClickOnce.Subscribe(_ =>
        {
            SEManager.Instance.Play(SEPath.DECISION);
            SceneManager.LoadScene($"Stage{_stageNo}_{_createdByName}");
        });
    }
}
