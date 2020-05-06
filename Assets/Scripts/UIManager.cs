using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //enble the use of UI element

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField] private Image _livesImages;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;

    private GameManager _gamemanager ;


    [SerializeField] private Sprite[] _liveSprites;



    // Start is called before the first frame update
    void Start()
    {
        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gamemanager == null)
        {
            Debug.LogError("Game_Manager is null!");
        }
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _livesImages.sprite = _liveSprites[3];
        //assign text component to the handle  
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score :" + score;
    }

    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the currentLives index
        _livesImages.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlickerRoutine());
        _gamemanager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        } 
    }
}

