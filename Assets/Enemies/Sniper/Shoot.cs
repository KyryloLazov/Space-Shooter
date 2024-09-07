using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : BaseState
{
    private GameObject _player;
    private float _waitForSeconds;
    private float _deadline;
    public override void EnterState(Sniper sniper, float speed)
    {
        _player = GameObject.Find("Player");
        _waitForSeconds = Random.Range(3f, 7f);
        _deadline = _waitForSeconds + Time.time;
    }

    public override void UpdateState(Sniper sniper, float speed)
    {
        if (Time.time <= _deadline && _player != null)
        {           
            Vector2 direction = ((Vector2)_player.transform.position - (Vector2)sniper.transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var offset = 90f;
            sniper.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
            sniper.Shoot();

        }
        else if(_player == null)
        {
            sniper.transform.rotation = Quaternion.identity;
        }
        else
        {
            sniper.transform.rotation = Quaternion.identity;
            sniper.SwitchState(sniper._changePos);
        }

    }

    public override void ExitState(Sniper sniper, float speed)
    {

    }

}
