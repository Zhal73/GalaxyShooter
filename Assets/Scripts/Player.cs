using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Screen Parameters
    [SerializeField] private float _screenLeftBorder = -9.5f;
    [SerializeField] private float _screenRightBorder = 9.5f;
    [SerializeField] private float _upperBound = 0f;
    [SerializeField] private float _lowerBound = -4.5f;

    //movement speed
    [SerializeField] private float _speed = 5.0f;

    //Laser Parameter s
    [SerializeField] private float _laserOffset = 1.03f;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _canFire = -1.0f;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;

    [SerializeField] private int _lives = 3;
    
    private float _speedMultiplier = 2.5f;

    //variable for isTripleShotActive??
    [SerializeField]  private bool _isTripleShotActive = false;

    [SerializeField] private bool _isShieldActive = false;

    //variable reference to the shield visualiser
    [SerializeField] private GameObject _shieldVisualiser;

    [SerializeField] private GameObject _rightEngineVisaliser;
    [SerializeField] private GameObject _leftEngineVisaliser;

    private SpawnManager _spawnManager;   //stores the reference for a spanwmanger object

    [SerializeField] private int _score = 0; //hold the score

    private UIManager _uiManager;


    //variable to store audio clip
    [SerializeField] private AudioSource _laserShot;

    [SerializeField] private GameObject _explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //set the current position to new position (0,0,0)  
        transform.position = new Vector3(0, 0, 0);
        _shieldVisualiser.SetActive(false);
        _rightEngineVisaliser.SetActive(false);
        _leftEngineVisaliser.SetActive(false);

        //assign the component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        //check if the SpawnManager was successfully grabbed
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        
    }

    void FireLaser()
    {
        // next opportunity to fire
        _canFire = Time.time + _fireRate;
        //instantiate tripel shot if tripleshot is active
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //instantiate the laser object with on offset
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
        }

        //play sound effect
        _laserShot.Play();
    }

    /*
   * Method to calculate the player's movement
   */
    void CalculateMovement()
    {
        /*
        * Allows user to use the keyboard to move a GameObject
        * by accessing the Input Horizontal Axis
        */
        float horizonatalInput = Input.GetAxis("Horizontal");

        /*
         * Allows user to use the keyboard to move a GameObject
         * by accessing the Input Vertical Axis
         */
        float verticalInput = Input.GetAxis("Vertical");



         /*
         * Horizontal movement
         */
         transform.Translate(Vector3.right * horizonatalInput * _speed * Time.deltaTime);

         /*
          *  Vertical movement
          */
         transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        /*
         * Limits the y movement between upperBound and Lower bound
         */

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _lowerBound, _upperBound), 0);

        /*
         * Limits the x movement between leftBound and rightBound,
         * once the player reach a boundary, it reappears on 
         * the opposite side.
         */
        if (transform.position.x > _screenRightBorder)
        {
            transform.position = new Vector3(_screenLeftBorder, transform.position.y, 0);
        }
        else if (transform.position.x < _screenLeftBorder)
        {
            transform.position = new Vector3(_screenRightBorder, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        // if shield active do nothing
        // and deactivate shield
        if(_isShieldActive)
        {
            _isShieldActive = false;
            //disable visualiser
            _shieldVisualiser.SetActive(false);
            return;
        }

        _lives--;
        //if lives = 2 show right engine damage
        //else if lives is 1 enable left engine damamge
        if(_lives == 2)
        {
            _rightEngineVisaliser.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEngineVisaliser.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if(_lives< 1)
        {
            _spawnManager.OnPlayerDeath();   //actual communication between SpawnManager and Player
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        //start powerdownd coroutine to disable triple shot
        //after 5 seconds
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRoutine
    //wait 5 sec
    //set triple shot to false
    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedUpActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedUpPowerDownoutine());
    }

    IEnumerator SpeedUpPowerDownoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        //enable visualiser
        _shieldVisualiser.SetActive(true);
        StartCoroutine(ShieldPowerDownoutine());
    }

    IEnumerator ShieldPowerDownoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualiser.SetActive(false);
        _isShieldActive = false;
    }

    //method to add points to the  score
    //communicate with the UI and update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
