using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        
        // 上に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.1f, 0.0f);
        }
        // 前に移動
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        // 後に移動
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
        // 左に回転
        if (Input.GetKey(KeyCode.A ))
        {
            myTransform.Rotate(0.0f, -1.0f, 0.0f, Space.World);
        }
        // 右に回転
        if (Input.GetKey(KeyCode.D))
        {
            myTransform.Rotate(0.0f, 1.0f, 0.0f, Space.World);
        }
    }
}
