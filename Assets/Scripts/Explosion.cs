using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioClip _explosionSound;

    // Start is called before the first frame update
    void Start()
    {   
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExplosionSound()
    {
      //  _explosionSound.Play();
    }
}
