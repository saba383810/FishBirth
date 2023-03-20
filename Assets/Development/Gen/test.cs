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

        
        // ã‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.1f, 0.0f);
        }
        // ‘O‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        // Œã‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
        // ¶‚É‰ñ“]
        if (Input.GetKey(KeyCode.A ))
        {
            myTransform.Rotate(0.0f, -1.0f, 0.0f, Space.World);
        }
        // ‰E‚É‰ñ“]
        if (Input.GetKey(KeyCode.D))
        {
            myTransform.Rotate(0.0f, 1.0f, 0.0f, Space.World);
        }
    }
}
