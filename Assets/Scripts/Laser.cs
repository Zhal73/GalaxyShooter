using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField] private float _laserSpeed = 8;

    // Update is called once per frame
    void Update()
    {
        //translate laser up  
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        // if the position on the y is greather than 8
        //destroy the object.
        if(transform.position.y >= 8)
        {
            //check if the object has a parent 
            //if it has then destroy the parent too
            if(transform.parent !=null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
        
    }


}
