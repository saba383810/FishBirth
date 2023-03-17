using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Blink : MonoBehaviour
{
    
    void Start()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null) return;
        DOTween.Sequence()
            .OnStart(() =>
            {
                canvasGroup.alpha = 0;
            })
            .Append(canvasGroup.DOFade(1, 1))
            .AppendInterval(1)
            .Append(canvasGroup.DOFade(0, 1))
            .SetLoops(-1,LoopType.Restart);
        
    }
}
