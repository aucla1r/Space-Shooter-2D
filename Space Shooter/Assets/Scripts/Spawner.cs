using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool _stopSpawning = false;
    [SerializeField] private float _wait = 5f;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyPrevab;
    [SerializeField] private GameObject _powerupContainer;
    [SerializeField] private GameObject[] _powerup;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while(_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, 3);
            GameObject powerup = Instantiate(_powerup[randomPowerup], new Vector3(Random.Range(-11.3f, 11.3f), 8f, 0), Quaternion.identity);
            powerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(4f, 7f));
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while(_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrevab, new Vector3(Random.Range(-11.3f, 11.3f), 8f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_wait);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
