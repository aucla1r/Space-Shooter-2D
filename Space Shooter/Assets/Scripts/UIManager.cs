using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText; 
    [SerializeField] private Text _gameOverText; 
    [SerializeField] private Text _restartKeyText; 
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _liveSprite;
    [SerializeField] private bool _gameActive = true;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score : 0"; 
        _gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AddScores();
        RestartGame();
    }

    private void AddScores()
    {
        if (_player != null)
        {
            _scoreText.text = "Score : " + _player.scores;   
        }   
    }

    public void UpdateLife(int currentLive)
    {
        _livesImage.sprite = _liveSprite[currentLive];
    }

    public void GameOver()
    {
        _restartKeyText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (_gameActive == true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void RestartGame()
    {
        if (_player.restartGame == true)
        {
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
