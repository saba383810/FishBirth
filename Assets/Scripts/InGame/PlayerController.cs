using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace InGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private GameController gameController;
        public void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("ClearPoint")) Finish(col.transform.position);
        }

        public void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Enemy")) PlayerDamage(1);
            if (col.gameObject.CompareTag("Score100")) ScoreUp(100,col.gameObject) ;
            if (col.gameObject.CompareTag("Death"))PlayerDamage(3);
        }

        public void Finish(Vector3 worldPos)
        {
            transform.position = worldPos;
            
            playerMove.rb.isKinematic = true;
            playerMove.SetPlaying(false);
            
            gameController.Finish(worldPos,playerMove.transform).Forget();
        }

        public void ScoreUp(int val,GameObject colObj)
        {
            gameController.AddScore(val);
            Destroy(colObj);
        }

        public void PlayerDamage(int val)
        {
            gameController.DamageHp(val);
        }
    }
}
