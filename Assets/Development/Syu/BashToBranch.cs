using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BashToBranch : MonoBehaviour
{
    
    public Rigidbody Rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rb.AddForce(transform.forward * 200); 
    }
}
