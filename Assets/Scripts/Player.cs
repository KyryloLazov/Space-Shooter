using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _fireRate;
    private float _newFire = -1f;

    [SerializeField]
    public int _lives = 3;

    public int _score;

    [SerializeField]
    private GameObject _laser;

    [SerializeField]
    public float _PowerUpDuration;

    [SerializeField]
    private GameObject _TripleShot;
    [SerializeField]
    private GameObject _Shield;
    [SerializeField]
    private float _SpeedUp;

    private bool _isTripleShot = false;
    private bool _isShield;

    [SerializeField]
    private GameObject _damage1;
    [SerializeField]
    private GameObject _damage2;

    [SerializeField]
    private AudioClip _laserSound;

    [SerializeField]
    private SpriteRenderer _playerSprite;

    private SpawnBehaviour _spawnBehaviour;
    private UIManager _uimanager;
    private GameController _gameController;
    private AudioSource _audioController;
    private bool _isInv = false;
    private SpriteRenderer[] _sprites;
    
    void Start()
    {
        transform.position = Vector3.zero;

        _spawnBehaviour = GameObject.Find("SpawnManager").GetComponent<SpawnBehaviour>();

        if(_spawnBehaviour == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        _Shield.SetActive(false);

        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uimanager == null)
        {
            Debug.LogError("UI is NULL");
        }

        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if(_gameController == null)
        {
            Debug.LogError("Game Controller is NULL");
        }
        _audioController = GetComponent<AudioSource>();
        if(_audioController == null)
        {
            Debug.LogError("Player Audio Source is NULL");
        }
        else
        {
            _audioController.clip = _laserSound;
        }
        _sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _newFire)
        {
            Shoot();
        }
    }      

    void Shoot()
    {
        _newFire = Time.time + _fireRate;
        if (_isTripleShot)
        {
            Instantiate(_TripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
        }
        _audioController.Play();
              
    }

    void Movement()
    {
        float GetAxisX = Input.GetAxis("Horizontal");
        float GetAxisY = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(GetAxisX, GetAxisY, 0);
        transform.Translate(Direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (!_isShield && !_isInv)
        {
            _lives -= 1;
            _uimanager.UpdateLives(_lives);
            StartCoroutine(IFrames());
            StartCoroutine(PlayerFlicker());

            if (_lives == 2)
            {
                _damage1.SetActive(true);
                _sprites = GetComponentsInChildren<SpriteRenderer>();
                StartCoroutine(IFrames());
            }
            else if (_lives == 1)
            {               
                _damage2.SetActive(true);
                _sprites = GetComponentsInChildren<SpriteRenderer>();
                StartCoroutine(IFrames());
            }
                                  
            else
            {
                _spawnBehaviour.StopSpawn();
                _gameController.GameOver();
                _uimanager.CheckHighScore(_score);
                Destroy(this.gameObject);               
            }
        }
        else if(_isShield)
        {
            _isShield = false;
            _Shield.SetActive(false);
        }
    }

    public void Repair()
    {
        _lives += 1;
        _uimanager.UpdateLives(_lives);

        if(_lives == 3)
        {
            _damage1.SetActive(false);
        }
        else if( _lives == 2)
        {
            _damage2.SetActive(false);
        }
        
    }

    IEnumerator IFrames()
    {
        _isInv = true;
        yield return new WaitForSeconds(2f);
        _isInv = false;
    }

    IEnumerator PlayerFlicker()
    {
        while(_isInv )
        {
            for(int i = 0; i<_sprites.Length; i++)
            {
                _sprites[i].enabled = false;
            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < _sprites.Length; i++)
            {
                _sprites[i].enabled = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShot = true;
        StartCoroutine(TripleShotCountDown());
    }

    IEnumerator TripleShotCountDown()
    {
        yield return new WaitForSeconds(_PowerUpDuration);
        _isTripleShot = false;
    }

    public void ActivateSpeedUp()
    {
        _speed += _SpeedUp;
        StartCoroutine(SpeedUpCountDown());
    }

    IEnumerator SpeedUpCountDown()
    {
        yield return new WaitForSeconds(_PowerUpDuration);
        _speed -= _SpeedUp;
    }

    public void ActivateShield()
    {
        _isShield = true;
        _Shield.SetActive(true);
    }

    public void ScoreUp(int points)
    {
        _score += points;      
        _uimanager.UpdateScore(_score);
    }

    public void ScoreDown(int points)
    {
        _score -= points;
        _uimanager.UpdateScore(_score);
    }
}
