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
        //Componentを取得
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
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(RordLorersound);
            Destroy(this.gameObject);
        }

    }
}
