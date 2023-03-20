using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool KeyGet = false;

    // Start is called before the first frame update
    void Start()
    {
        KeyGet = false;
    }
    void Update()
    {
     
    }
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            KeyGet = true;
            Destroy(gameObject);
        }
    }
}
