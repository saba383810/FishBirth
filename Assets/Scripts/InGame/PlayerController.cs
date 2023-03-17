using System;
using System.Collections;
using System.Collections.Generic;
using InGame;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private GameController gameController;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("ClearPoint")) Finish(col.transform.position);
    }
    
    public void Finish(Vector3 worldPos)
    {
        transform.position = worldPos;
        
        playerMove.rb.isKinematic = true;
        playerMove.SetPlaying(false);
        
        gameController.Finish(worldPos,playerMove.transform);
    }
}
