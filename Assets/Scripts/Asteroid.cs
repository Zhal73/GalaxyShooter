using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 19.0f;
    [SerializeField] private GameObject _explosionPrefab;
    private SpawnManager _spawnmanager;



    // Start is called before the first frame update
    void Start()
    {
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime); 
    }

    //check for laser collision
    //instantiate explosion a t the ateroid's position
    //destroy explosion after 3 sec.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            
            Instantiate(_explosionPrefab, transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            _spawnmanager.StartSpawning();
            Destroy(this.gameObject,0.1f);
            
        }
    }
 }
