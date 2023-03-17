using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace sabanogames.Common.UI
{
    public enum ButtonSeType
    {
        Ok,
        Cancel,
        Tap,
        None
    }
    public class CommonButton : MonoBehaviour , IButtonInput
    {
        [SerializeField, Header("クリックイベントに使用したいButton")]
        private Button button;
        [SerializeField,Header("ButtonのSE")] 
        private ButtonSeType buttonSe = ButtonSeType.Ok;

        private readonly TimeSpan _reInputSpan = TimeSpan.FromMilliseconds(1000);

        private void Awake()
        {
            if (button == null)
            {
                button = gameObject.GetComponent<UnityEngine.UI.Button>();
            }
        }

        /// <summary>シンプルなボタン</summary>
        public IObservable<Unit> OnClick => button.OnClickAsObservable();

        /// <summary>連打防止用のbuttonの実装(連打感覚は _reInputSpanの期間)</summary>
        public IObservable<Unit> OnClickDefendChattering => button.OnClickAsObservable().ThrottleFirst(_reInputSpan);

        /// <summary>一回だけ押せるボタンの実装</summary>
        public IObservable<Unit> OnClickOnce => button.OnClickAsObservable().First();
        
        /// <summary>
        /// Buttonを有効または無効にする (Disabledステータスになるので見た目が変わる)
        /// </summary>
        /// <param name="isActive"></param>
        public void SetInteractable(bool isActive)
        {
            button.interactable = isActive;
        }

        /// <summary>
        /// Buttonを有効または無効にする(色は変わらない)
        /// </summary>
        /// <param name="isActive"></param>
        public void SetEnabled(bool isActive)
        {
            button.enabled = isActive;
        }

        private void Reset()
        {
            button = GetComponentInChildren<Button>();
        }
    }
}

