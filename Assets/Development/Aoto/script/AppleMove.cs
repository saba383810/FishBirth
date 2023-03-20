using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;


public class AppleMove : MonoBehaviour
{

    Vector3 pos;

    GameObject player;

    float playerDistance;

    public float speed;

    private bool _isDrop;
    private Vector3 tmpPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        pos = gameObject.transform.position;
        tmpPos = pos;
        player = GameObject.Find("sabato");
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (playerDistance < 2.5f)
        {
            _isDrop = true;
        }
    }


    private void FixedUpdate()
    {
        if (_isDrop)
        {
            pos.y -= 0.1f;
            if (pos.y < -1.2)
            {
                _isDrop = false;
                transform.position = tmpPos;
                pos = tmpPos;
            }

            gameObject.transform.position = pos;
        }
    }
}
