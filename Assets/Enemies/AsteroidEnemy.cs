using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy
{
    [SerializeField]
    GameObject asteroidPrefab;
    Vector3 spawnPos;
    [SerializeField]
    float XMult = 1;
    [SerializeField]
    float ScaleMult = 0.2f;
    [SerializeField]
    float SpeedMult = 1.1f;
    public int LifeLeft = 2;

    private void Start()
    {
        base.Init();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.4f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 7.4f, 0);
        }
    }

    public override void Death()
    {
        if (LifeLeft > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    spawnPos = new Vector3(transform.position.x + XMult, transform.position.y, transform.position.z);
                }
                else
                {
                    spawnPos = new Vector3(transform.position.x - XMult, transform.position.y, transform.position.z);
                }
                GameObject AsteroidJR = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
                AsteroidJR.transform.localScale = new Vector3(AsteroidJR.transform.localScale.x - ScaleMult, AsteroidJR.transform.localScale.y - ScaleMult, 1);
                AsteroidEnemy asteroid = AsteroidJR.GetComponent<AsteroidEnemy>();
                asteroid.LifeLeft -= 1;
                asteroid._speed *= SpeedMult;
            }
        }
        _player.ScoreUp(_pointCost);
        Destroy(gameObject);        
    }
}
