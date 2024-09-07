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
        base.Init();
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
