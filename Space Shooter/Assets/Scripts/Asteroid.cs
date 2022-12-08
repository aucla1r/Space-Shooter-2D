using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    private AudioSource _explosionAudio;
    [SerializeField] private float _speedRotation = 15.0f; 
    private Spawner _spawner;
    // Start is called before the first frame update
    void Start()
    {
        _explosionAudio = GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("_explossionAudio is NULL on Asteroid");
        }

        _spawner = GameObject.Find("Spawning_Manager").GetComponent<Spawner>();
        if (_spawner == null)
        {
            Debug.LogError("Get Spawner Object Error on Asteroid.cs");
        }

        transform.position = new Vector3(0, 3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _spawner.StartSpawning();
            _explosionAudio.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject, 5f);
            Destroy(Instantiate(_explosion, transform.position, Quaternion.identity), 3f);
        }    
    }
}
