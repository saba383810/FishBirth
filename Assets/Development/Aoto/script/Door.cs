using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private CanvasGroup keyNotFound;
    private bool _isDoorOpen;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Key.KeyGet)
            {
                OpenDoor();
            }
            else
            {
                DOTween.Sequence()
                    .OnStart(() =>
                    {
                        keyNotFound.alpha = 0;
                        keyNotFound.gameObject.SetActive(true);
                    })
                    .Append(keyNotFound.DOFade(1, 0.3f))
                    .AppendInterval(1)
                    .Append(keyNotFound.DOFade(0, 0.2f))
                    .OnComplete(() =>
                    {
                        keyNotFound.alpha = 0;
                        keyNotFound.gameObject.SetActive(false);
                    });
            }
        }
    }

    public void OpenDoor()
    {
        if(_isDoorOpen) return;
        _isDoorOpen = true;
        transform.DORotate(new Vector3(-90, -90, 0), 1f);
    }
}
