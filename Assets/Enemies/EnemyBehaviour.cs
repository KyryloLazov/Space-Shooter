using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Enemy
{
    void Update()
    {
        transform.Translate(Vector3.down* _speed * Time.deltaTime);
        
        if(transform.position.y < -5.4f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 7.4f, 0);
        }

        if(Time.time > _newFire && !_isDead)
        {
            _fireRate = Random.Range(3f, 7f);
            _newFire = Time.time + _fireRate;
            _Laser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            LaserBehaviour[] lasers = _Laser.GetComponentsInChildren<LaserBehaviour>();
        }
    }
}
