using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _topScreen = 6.4f;
    [SerializeField] private float _bottomScreen = -6.4f;
    [SerializeField] private float _screenLeftBorder = -9.5f;
    [SerializeField] private float _screenRightBorder = 9.5f;
    [SerializeField] private float _triplePowerUpSped = 3.0f;
    [SerializeField] private float _SpeedPowerUpSped = 3.0f;


    [SerializeField] private int _powerUpId;  // 0 = TripleShot 1 = Speed 2 = Shield


    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3 (adjustable in the inspector)
        transform.Translate(Vector3.down * _triplePowerUpSped * Time.deltaTime);
       // when off the screen destroy
       if(transform.position.y < _bottomScreen)
        {
            Destroy(this.gameObject);    
        }
    }

    //check collision only with the player
    //only be collectable by the player
    //once collected destroy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            //Communicate with the palyer script
            Player player = other.transform.GetComponent<Player>();
            
            switch(_powerUpId)
            {
                // if powerup is 0
                // tripleshot
                //if powerup is 1
                //speedup
                // if powerup is 2
                // shield
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedUpActive();
                    break;
                case 2:
                    player.ShieldActive();
                    break;
                default:
                    break;
            }
            
            Destroy(this.gameObject);

        }
    }
}
