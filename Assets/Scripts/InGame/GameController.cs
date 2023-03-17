using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using InGame;
using KanKikuchi.AudioManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private StageUIView stageUIView;
    [SerializeField] private ClearUIView clearUiView;
    
    [Header("Player")]
    [SerializeField] private PlayerMove playerMove;
    
    [Header("ClearPoint")]
    [SerializeField] private GameObject subCamera;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private FishMove   fishMove;
    
    private PlayerData _playerData;
    private CancellationTokenSource _cts;

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
    }
    public async UniTask Finish(Vector3 worldPos,Transform playerTransform)
    {
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

    public void AddScore(int val)
    {
        _playerData.Score += val;
        stageUIView.SetScoreView(_playerData.Score);
        SEManager.Instance.Play(SEPath.SCORE_UP);
    }

    public void DamageHp(int val)
    {
        _playerData.Hp -= val;
        stageUIView.SetHpView(_playerData.Hp);
        SEManager.Instance.Play(SEPath.DAMAGE);
        
        if (_playerData.Hp <= 0) GameOver();
    }

    public void  GameOver()
    {
        
    }

    public void OnDestroy()
    {
        _cts?.Cancel();
    }
}
