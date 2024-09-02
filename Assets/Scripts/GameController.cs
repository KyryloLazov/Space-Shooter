using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool _isDead;
    [SerializeField]
    private GameObject PauseMenu;
    private Animator _animatorPause;
    private SpawnBehaviour _spawnBehaviour;

    private static GameController _instance;

    public static GameController Instance
    {
        get 
        { 
            if( _instance == null)
            {
                Debug.LogError("GameConroller is not present!");
            }
            return _instance; 
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _isDead = false;
        _animatorPause = PauseMenu.GetComponent<Animator>();
        _animatorPause.updateMode = AnimatorUpdateMode.UnscaledTime;
        _spawnBehaviour = GameObject.Find("SpawnManager").GetComponent<SpawnBehaviour>();
    }

    void Update()
    {
        if(_isDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        { 
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            _animatorPause.SetBool("isPaused", true);
        }

        
    }

    public void GameOver()
    {
        _isDead = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        _animatorPause.SetBool("isPaused", false);
        PauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
