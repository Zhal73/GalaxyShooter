using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4.0f;
    [SerializeField] private float _topScreen = 6.4f;
    [SerializeField] private float _bottomScreen = -6.4f;
    [SerializeField] private float _screenLeftBorder = -9.5f;
    [SerializeField] private float _screenRightBorder = 9.5f;

    private Player _player;

    //handle to animator componenet
    private Animator _enemyDestruction;

    [SerializeField] private GameObject _explosionPrefab;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is null!!");
        }
        // assign componenet to anim
        _enemyDestruction = GetComponent<Animator>();
        if(_enemyDestruction == null)
        {
            Debug.LogError("_enemyDestruction is null!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meter per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //if bottom of screen 
        //respawn at top with a new random x position
        if (transform.position.y <= _bottomScreen)
        {
            float randomX = Random.Range(_screenLeftBorder, _screenRightBorder);
            transform.position = new Vector3(randomX, _topScreen, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        // damage the player
        // destroy us
        if(other.CompareTag("Player"))
        {
           _player.AddScore(10);
           _player.Damage();
            //trigger anim
           Destruction();
           Destroy(this.gameObject,2.5f);
        }

        // if other is laser
        // destroy laser
        // destroy us
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            //add 10 to score
            if(_player != null)
            {
                _player.AddScore(10);
            }
            //trigger anim
            Destruction();
            Destroy(this.gameObject, 2.5f);
        }

    }
    private void Destruction()
    {
        _enemyDestruction.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }
}
