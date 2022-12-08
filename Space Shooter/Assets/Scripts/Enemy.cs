using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    private AudioSource _explosionAudio;
    private Animator _enemyDestroyedAnim;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private float _speed = 4.0f;
    float xRandom;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_Player is NULL");
        }

        _boxCollider2D = GetComponent<BoxCollider2D>();
        if (_boxCollider2D == null)
        {
            Debug.LogError("Get Box Collider = Error");
        }

        _enemyDestroyedAnim = GetComponent<Animator>();
        if (_enemyDestroyedAnim == null)
        {
            Debug.LogError("Error Animation Destroy Enemies");
        }

        _explosionAudio = GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("_explossionAudio is NULL on Enemy");
        }

        xRandom = Random.Range(-9.4f, 9.4f); 
        Vector3 pos = new Vector3(xRandom, 7.4f, 0);   
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        xRandom = Random.Range(-9.4f, 9.4f); 
        float yPos = -1 * _speed * Time.deltaTime; 

        transform.Translate(0, yPos, 0);
        if(transform.position.y <= -7.4f)
        {
            transform.position = new Vector3(xRandom, 7.4f , 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            _explosionAudio.Play();
            if (player != null)
            {
                player.Damage();
            }
            EnemyDestroyedSequence();
        }
        
        if (other.tag == "Laser")
        {
            Destroy(GameObject.FindWithTag("Laser"));
            _explosionAudio.Play();
            
            if (_player != null)
            {
                _player.AddPlayerScores();
            }
            EnemyDestroyedSequence();
        }
        
    }

    private void EnemyDestroyedSequence()
    {
        Destroy(this._boxCollider2D);
        _speed = 0;
        _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 2.3f);
    }
}
