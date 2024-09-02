using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private Text _waveCounterText;
    [SerializeField]
    private Text _highScoreText;
    private int _highScore;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("UIManager is not present!");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _highScore = PlayerPrefs.GetInt("Highscore");
        _highScoreText.text = "High: " + _highScore;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _waveCounterText.gameObject.SetActive(false);
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void CheckHighScore(int score)
    {
        if (_highScore < score)
        {
            _highScore = score;
            _highScoreText.text = "High: " + score;
            PlayerPrefs.SetInt("Highscore", _highScore);
        }
    }

    public void ShowWave(int wave)
    {
        _waveCounterText.text = "Wave " + wave;
        StartCoroutine(WaveText(_waveCounterText));

    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprite[currentLives];

        if(currentLives <= 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(TextFlicker(_gameOverText));           
        }
    }
    IEnumerator TextFlicker(Text text)
    {
        while(true)
        {         
            yield return new WaitForSeconds(0.7f);
            text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
           text.gameObject.SetActive(true);         
        }
    }

    IEnumerator WaveText(Text text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);

    }

    public void Resume()
    {
        GameController.Instance.Unpause();
    }

    public void MainMenu() {
        GameController.Instance.Unpause();
    }
}
