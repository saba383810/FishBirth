using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RordLorerBashSwith : MonoBehaviour
{
    public GameObject RordLorer;
    public AudioClip RordLorersound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "sabato")
        {
            RordLorer.gameObject.SetActive(true);
            //‰¹(sound1)‚ð–Â‚ç‚·
            audioSource.PlayOneShot(RordLorersound);
            Destroy(this.gameObject);
        }

    }
}
