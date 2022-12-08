using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f; 
    [SerializeField] private GameObject _laserPrefab;      
    [SerializeField] private GameObject _tripleShotPrefab;   
    [SerializeField] private GameObject _shield;   
    [SerializeField] private GameObject _leftEngine;   
    [SerializeField] private GameObject _rightEngine;   
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _wait = 5f;
    private Spawner _spawner;
    private AudioSource _laserAudio;
    private float _canFire = -1f;
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] public bool _isShieldActive = false;
    [SerializeField] public bool restartGame = false;
    public int scores;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        
        _spawner = GameObject.Find("Spawning_Manager").GetComponent<Spawner>();
        if (_spawner == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }   

        _laserAudio = GetComponent<AudioSource>();
        if (_laserAudio == null)
        {
            Debug.LogError("_laserAudio is NULL");
        }

    }

    void Update()
    {
        CalculateMovement();
        
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
          FireLaser();
        }
    }

    void CalculateMovement()
    {   
        SpeedBoost();
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if(transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;    
          if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
            }
        _laserAudio.Play();
    }

    void SpeedBoost()
    {
        float horizontalInput = Input.GetAxis("Horizontal");     
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive == true)
        {
            _speed = 8.5f;
            transform.Translate(direction * _speed * Time.deltaTime);     
        }
        else
        {
            _speed = 3.5f;
            transform.Translate(direction * _speed * Time.deltaTime);   
        }
        
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerdownRoutine());
    }

    IEnumerator SpeedBoostPowerdownRoutine()
    {
        yield return new WaitForSeconds(_wait);
        _isSpeedBoostActive = false;
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_wait);
        _isTripleShotActive = false;   
    }


    public void ShieldActive()
    {
        _shield.SetActive(true);
        _isShieldActive = true;
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            return;
        }
        else
        {
            _lives--;
            if (_lives == 2)
            {
                _leftEngine.SetActive(true);
            }

            else if(_lives == 1)
            {
                _rightEngine.SetActive(true);
            }

            uiManager.UpdateLife(_lives);
            if (_lives < 1)
            {
                _spawner.OnPlayerDeath();
                Destroy(this.gameObject);
                uiManager.GameOver();
                restartGame = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            _isShieldActive = false;
            _shield.SetActive(false);
        }
    }

    public void AddPlayerScores()
    {
        int scoreMin = 50;
        int scoreMax = 100;
        scores += Random.Range(scoreMin, scoreMax);
    }
}