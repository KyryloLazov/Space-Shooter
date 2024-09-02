using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected int _pointCost;

    [SerializeField]
    protected float _fireRate;
    [SerializeField]
    protected float _newFire;

    [SerializeField]
    protected GameObject _enemyLaser;

    protected Animator _animator;
    protected Player _player;
    protected Collider2D _collider;
    protected AudioSource _explosion;
    protected GameObject _Laser;
    protected bool _isDead;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_player != null)
        {
            if (other.tag == "Player")
            {
                _player.Damage();
                _animator.SetTrigger("Death");
                _speed = 0;
                _collider.enabled = false;

                _isDead = true;
                _explosion.Play();
                Destroy(this.gameObject, 3f);
            }
            else if (other.tag == "Laser")
            {
                _player.ScoreUp(_pointCost);
                _animator.SetTrigger("Death");
                _speed = 0;
                _collider.enabled = false;

                _isDead = true;
                _explosion.Play();
                Destroy(this.gameObject, 3f);
                Destroy(other.gameObject);
            }
        }
    }
}
