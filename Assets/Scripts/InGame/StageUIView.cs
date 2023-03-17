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

        private const string HpSpritePath = "Image/HP/";
        private int MaxHp { get; set; }
        
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
            // ScoreTextの変更
            scoreText.text = score.ToString("000000");
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
    }
}
