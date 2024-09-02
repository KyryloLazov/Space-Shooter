using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Enemy
{
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _collider = GetComponent<Collider2D>();

        if(_collider == null)
        {
            Debug.LogError("Collider is NULL");
        }
        _explosion = GetComponent<AudioSource>();
        if(_explosion == null)
        {
            Debug.LogError("Enemy Audio Source is NULL");
        }
    }

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
