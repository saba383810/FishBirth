using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace InGame
{
    public class GameController : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private StageUIView stageUIView;
        [SerializeField] private ClearUIView clearUiView;
        [SerializeField] private GameOverUIView gameOverUIView;
    
        [SerializeField] private CanvasGroup damageFlash;
    
        [Header("Player")]
        [SerializeField] private PlayerMove playerMove;
    
        [Header("ClearPoint")]
        [SerializeField] private GameObject subCamera;
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private FishMove   fishMove;
        [Header("GameOver Timer")]
        [SerializeField] private int gameOverTimer = 60;
    
        private PlayerData _playerData;
        private CancellationTokenSource _cts;
        private bool _isDamage;
        private bool _isTimeStop;

        void Start()
        {
            Application.targetFrameRate = 60;
            BGMManager.Instance.Play(BGMPath.STAGE);
            _cts = new CancellationTokenSource();
            _playerData = new PlayerData();
            _playerData.Init();
            StartEffect();
        }

        void StartEffect()
        {
            // StartのUIを表示
            stageUIView.Init(_playerData);
        
            // TODO: StartのEffectを追加
        
            // Playerの移動を許可
            playerMove.SetPlaying(true);
            StartGameOverTimer(_cts.Token).Forget();
        }

        public void AddScore(int val)
        {
            _playerData.Score += val;
            stageUIView.SetScoreView(_playerData.Score);
            SEManager.Instance.Play(SEPath.SCORE_UP);
        }

        public void DamageHp(int val)
        {
            if(_isDamage) return;
            _isDamage = true;
            _playerData.Hp -= val;
            stageUIView.SetHpView(_playerData.Hp);
            SEManager.Instance.Play(SEPath.DAMAGE);
            DOTween.Sequence()
                .OnStart(()=>
                {
                    damageFlash.alpha = 0;
                    damageFlash.gameObject.SetActive(true);
                })
                .Append(damageFlash.DOFade(0.5f, 0.1f))
                .Append(damageFlash.DOFade(0f, 0.1f))
                .OnComplete(()=>damageFlash.gameObject.SetActive(false));
        
        
            if (_playerData.Hp <= 0) GameOver();
            // 1秒間はダメージを受けない
            Invoke(nameof(DamageEnd),1);
        
        }

        public void DamageEnd()
        {
            _isDamage = false;
        }
    
        public async UniTask Finish(Vector3 worldPos,Transform playerTransform)
        {
            _isTimeStop = true;
            // カメラを設定
            stageUIView.gameObject.SetActive(false);
            subCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
        
            // フィニッシュアニメーション
            fishMove.FishFinishAnimation(worldPos,playerTransform).Forget();

            await UniTask.Delay(TimeSpan.FromSeconds(5),cancellationToken:_cts.Token);
            SEManager.Instance.Play(SEPath.YEAH_MAN);
            SEManager.Instance.Play(SEPath.KIRAKIRA);
            clearUiView.Show(_playerData.Score);
        }

        public void  GameOver()
        {
            _isTimeStop = true;
            playerMove.gameObject.SetActive(false);
            playerMove.SetPlaying(false);
            gameOverUIView.Show();
        }

        public async UniTask StartGameOverTimer(CancellationToken token)
        {
            stageUIView.SetTimer(gameOverTimer);
            while (gameOverTimer > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1),cancellationToken:token);
                if (token.IsCancellationRequested||_isTimeStop) break;
                gameOverTimer -= 1;
                stageUIView.SetTimer(gameOverTimer);
            }
        
            if(!token.IsCancellationRequested && !_isTimeStop) GameOver();
        }
    
        public void OnDestroy() { _cts?.Cancel(); }
    }
}
