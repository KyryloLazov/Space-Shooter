using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _waitTime;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _sniper;
    [SerializeField]
    private GameObject _TripleShot;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField] 
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    private bool _isDead = false;
    public int _wave = 1;
    private int _numOfEnemies;
    private float _spawnRate = 1.5f;
    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        while (!_isDead)
        {
            _numOfEnemies = 5 + _wave * 2;
            for(int i = 0; i<=_numOfEnemies; i++)
            {
                int spawnChance = Random.Range(1, 5);
                SpawnNormal();

                if (spawnChance >= 4)
                {
                    SpawnSniper();
                }
                yield return new WaitForSeconds(_waitTime);
            }
            yield return new WaitForSeconds(_spawnRate);
            _wave += 1;
            //_spawnRate -= 0.1f;
            UIManager.Instance.ShowWave(_wave);
            //_uiManager.ShowWave(_wave);
            
            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3f);
        while (!_isDead)
        {
            float SpawnTime = Random.Range(3, 8);
            float RandomX = Random.Range(-8f, 8f);
            GameObject PowerUp = Instantiate(_powerUps[Random.Range(0, 3)], new Vector3(RandomX, 7.4f, 0), Quaternion.identity);
            PowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    private void SpawnNormal ()
    {
        float RandomX = Random.Range(-8f, 8f);
        GameObject Enemy = Instantiate(_enemy, new Vector3(RandomX, 7.4f, 0), Quaternion.identity);
        Enemy.transform.parent = _enemyContainer.transform;
    }

    private void SpawnSniper()
    {
        int LeftRight = Random.Range(0, 2);
        float X = 0;
        switch (LeftRight)
        {
            case 0:
                X = 12f;
                break;
            case 1:
                X = -12f;
                break;
        }

        float RandomY = Random.Range(2f, 6f);
        GameObject Enemy = Instantiate(_sniper, new Vector3(X, RandomY, 0), Quaternion.identity);
        Enemy.transform.parent = _enemyContainer.transform;
    }

    public void StopSpawn()
    {
        _isDead = true;
    }
}
