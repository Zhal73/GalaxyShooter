using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _topScreen = 6.4f;
    [SerializeField] private float _bottomScreen = -6.4f;
    [SerializeField] private float _screenLeftBorder = -9.5f;
    [SerializeField] private float _screenRightBorder = 9.5f;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    //[SerializeField] private GameObject _tripleShotPowerUpPrefab;
    //[SerializeField] private GameObject _SpeePowerUpPrefab;
    [SerializeField] private GameObject[] _powerUps; //list that contains powerups

    private bool _stopSpawning = false;      //false when the player is alive


    // Start is called before the first frame update
    void Start()
    {

    }   

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnTripleShotPowerUp());
        //StartCoroutine(SpawnSpeedPowerUp());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // changes the variable _stopSpawning when player dies
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    //spawn game object every 5 seconds
    //with the use of coroutines
    //Create a coroutine of type IEnumerator 
    //use yiels event
    //while loop
    IEnumerator SpawnEnemyRoutine()
    {
        // while loop (infinite loop)
        //instantiate enemy prefab
        //yeal wait for 5 seconds.
        yield return new WaitForSeconds(1.5f);
        while(_stopSpawning == false)
        {
            float randomX = Random.Range(_screenLeftBorder, _screenRightBorder);
            Vector3 posToSpawn = new Vector3(randomX, _topScreen, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn ,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    /*
    IEnumerator SpawnTripleShotPowerUp()
    {
        while(_stopSpawning == false)
        {
            //every 3-7 seconds spawn a triple shot powerup
            // at random x
            float randomTime = Random.Range(3.0f, 7.0f);
            float randomX = Random.Range(_screenLeftBorder, _screenRightBorder);
            Vector3 posToSpawn = new Vector3(randomX, _topScreen, 0);

            Instantiate(_tripleShotPowerUpPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
        
    }

    IEnumerator SpawnSpeedPowerUp()
    {
        while (_stopSpawning == false)
        {
            //every 3-7 seconds spawn a triple shot powerup
            // at random x
            float randomTime = Random.Range(3.0f, 7.0f);
            float randomX = Random.Range(_screenLeftBorder, _screenRightBorder);
            Vector3 posToSpawn = new Vector3(randomX, _topScreen, 0);

            Instantiate(_SpeePowerUpPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }

    }
    */
    IEnumerator SpawnPowerUpRoutine()
    {    
        yield return new WaitForSeconds(1.5f);
        while (_stopSpawning == false)
        {
            float randomTime = Random.Range(3.0f, 7.0f);
            int powerUpToSpawn = Random.Range(0, 3);
            float randomX = Random.Range(_screenLeftBorder, _screenRightBorder);
            Vector3 posToSpawn = new Vector3(randomX, _topScreen, 0);

            Instantiate(_powerUps[powerUpToSpawn], posToSpawn, Quaternion.identity);
            Debug.Log("powerup" + powerUpToSpawn);
            yield return new WaitForSeconds(randomTime);
        }
    }

}
