using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float _speed;
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

    public virtual void Init()
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
    }

    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (_player != null)
        {
            if (other.tag == "Player")
            {
                _player.Damage();
                Death();
                
            }
            else if (other.tag == "Laser")
            {              
                Destroy(other.gameObject);
                Death();
            }
        }
    }

    public virtual void Death()
    {
        _player.ScoreUp(_pointCost);
        _animator.SetTrigger("Death");
        _speed = 0;
        _collider.enabled = false;
        _isDead = true;
        _explosion.Play();
        Destroy(this.gameObject, 3f);
    }
}
