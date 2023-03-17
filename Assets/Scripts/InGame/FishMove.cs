using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FishMove : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private Transform playerParent;
    public async UniTask FishFinishAnimation(Vector3 worldPos,Transform playerTransform)
    {
        transform.position = worldPos;
        playerTransform.SetParent(playerParent);
        playerTransform.localPosition = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
        
        foreach (var particle in particleSystems)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }

        var pos = transform.position;
        //TODO 魚音源いれる
        DOTween.Sequence()
            .AppendInterval(1)
            .Append(transform.DORotate(new Vector3(0, 0, -50), 0.5f))
            .Join(transform.DOMove(new Vector3(pos.x - 1, pos.y + 1, pos.z), 0.5f))
            .Append(transform.DORotate(new Vector3(0, 0, -90), 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear)
                .SetLoops(3, LoopType.Incremental))
            .Join(transform.DOMove(new Vector3(pos.x - 1.5f, pos.y + 1.5f, pos.z), 0.6f))
            .Append(transform.DORotate(new Vector3(0, 0, 50), 0.2f))
            .Join(transform.DOMove(new Vector3(pos.x - 1.7f, pos.y + 1.7f, pos.z), 0.2f))
            .Append(transform.DOMove(new Vector3(pos.x - 6, pos.y - 4, pos.z), 6f))
            .Join(transform.DORotate(new Vector3(0, 0, 90), 2f));
    }
}
