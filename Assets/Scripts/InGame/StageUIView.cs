using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class StageUIView : MonoBehaviour
    {
        [Header("ScoreView")]
        [SerializeField] private Text scoreText;
        
        [Header("HPView")]
        [SerializeField] private Text  hpText;
        [SerializeField] private Image hpImage;

        [Header("TimerView")] 
        [SerializeField] private Text timeText;
        
        private const string HpSpritePath = "Image/HP/";
        private int MaxHp { get; set; }
        private int _currentScore;
        
        public void Init(PlayerData playerData)
        {
            SetScoreView(playerData.Score);
            SetMaxHP(playerData.MaxHp);
            SetHpView(playerData.Hp);
        }

        /// <summary>
        ///    ScoreのUITextの値を変更する
        /// </summary>
        public void SetScoreView(int score)
        {
            DOTween.To(() => _currentScore, (val) =>
            {
                _currentScore = val;
                scoreText.text = val.ToString("000000");
            }, score, 0.5f);
        }

        /// <summary>
        ///    HPのUITextと画像の値を変更する
        /// </summary>
        public void SetHpView(int hp)
        {
            // HPTextの変更
            hpText.text = $"{hp}/{MaxHp}";
            
            // TODO: 魚の柄変更
            var hpSprite = Resources.Load<Sprite>($"{HpSpritePath}HP_{hp}");
            if(hpSprite == null) return;
            hpImage.sprite = hpSprite;
          
        }
        
        /// <summary>
        ///    HPの最大値を変更する
        /// </summary>
        public void SetMaxHP(int maxHp)
        {
            MaxHp = maxHp;
        }

        public void SetTimer(int val)
        {
            if (val <= 5)
            {
                SEManager.Instance.Play(SEPath.COUUNT_DOWN);
                timeText.color = Color.red;
                DOTween.Sequence()
                    .OnStart(() => timeText.transform.localScale = Vector3.one)
                    .Append(timeText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f).SetEase(Ease.OutQuint))
                    .Append(timeText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuint));
            }
            timeText.text = val.ToString("00");
        }
    }
}
