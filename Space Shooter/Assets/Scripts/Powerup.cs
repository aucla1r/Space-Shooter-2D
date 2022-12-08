using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private AudioSource _audioSource; 
    [SerializeField] private int _powerupID;
    [SerializeField] private float _speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(Random.Range(-9.1f, 9.1f), 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            _audioSource.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 3f);
        }
    }
}
