using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMove : MonoBehaviour
{ 
    GameObject player;
    bool isfall;
    float playerDistance;
    void Start()
    {
        player = GameObject.Find("sabato");
        isfall = false;
    }
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (playerDistance < 2.5f)
        {
            isfall = true;
        }
        if(isfall == true)
        {
            
        }
    }
}
