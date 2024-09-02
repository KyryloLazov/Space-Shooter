using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sniper : Enemy
{
    private BaseState _currentState;
    public Move _move = new Move();
    public Shoot _shoot = new Shoot();
    public ChangePos _changePos = new ChangePos();


    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _collider = GetComponent<Collider2D>();

        if (_collider == null)
        {
            Debug.LogError("Collider is NULL");
        }
        _explosion = GetComponent<AudioSource>();
        if (_explosion == null)
        {
            Debug.LogError("Enemy Audio Source is NULL");
        }

        _currentState = _move;

        _currentState.EnterState(this, _speed);
    }

    void Update()
    {
        if (transform.position.x > 13f)
        {
            transform.position = new Vector3(-13f, transform.position.y, 0);
        }
        else if (transform.position.x < -13f)
        {
            transform.position = new Vector3(13f, transform.position.y, 0);
        }
        _currentState.UpdateState(this, _speed);       
    }

    public void SwitchState(BaseState state)
    {
        _currentState = state;
        state.EnterState(this, _speed);
    }
    public void Shoot() {
        if (Time.time > _newFire && !_isDead)
        {
            _newFire = Time.time + _fireRate;
            _Laser = Instantiate(_enemyLaser, transform.position, transform.rotation);
            LaserBehaviour[] lasers = _Laser.GetComponentsInChildren<LaserBehaviour>();
        }
    }
}
