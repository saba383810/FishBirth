using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace InGame
{
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

            var pos = transform.localPosition;
        
            DOTween.Sequence()
                .AppendCallback(()=>SEManager.Instance.Play(SEPath.FINISH2))
                .AppendInterval(1f)
                .AppendCallback(()=>SEManager.Instance.Play(SEPath.FINISH1))
                .Append(transform.DOLocalRotate(new Vector3(0, 0, -50), 0.5f))
                .Join(transform.DOLocalMove(new Vector3(pos.x - 1, pos.y + 1, pos.z), 0.5f))
                .AppendCallback(()=>SEManager.Instance.Play(SEPath.FINISH2))
                .Append(transform.DOLocalRotate(new Vector3(0, 0, -90), 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(3, LoopType.Incremental))
                .Join(transform.DOLocalMove(new Vector3(pos.x - 1.5f, pos.y + 1.5f, pos.z), 0.6f))
                .AppendCallback(()=>SEManager.Instance.Play(SEPath.FINISH2))
                .Append(transform.DOLocalRotate(new Vector3(0, 0, 50), 0.2f))
                .Join(transform.DOLocalMove(new Vector3(pos.x - 1.7f, pos.y + 1.7f, pos.z), 0.2f))
                .AppendCallback(()=>SEManager.Instance.Play(SEPath.FINISH3))
                .Append(transform.DOLocalMove(new Vector3(pos.x - 6, pos.y - 4, pos.z), 6f))
                .Join(transform.DOLocalRotate(new Vector3(0, 0, 90), 2f));
        }
    }
}
