using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    public static PlaySceneManager Instance { get; private set; }

    public float regenStartTime;
    public float[] regenIntervalTime;
    public GameObject[] obstacleArray;
    public Transform obstacleContainer;
    public GameObject endingPanel;

    public Sprite[] numberSpriteArray;
    public Image[] nowScoreArray;
    public Image[] highScoreArray;

    public AudioClip endAudioClip;

    private AudioSource playSceneAudio;
    private int highScore;
    
    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        playSceneAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(MakeObstacle());
        InitializeScore();
    }

    private void InitializeScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        SetNowScore(0);
        SetHighScore(highScore);
    }

    public void EndGame()
    {
        playSceneAudio.clip = endAudioClip;
        playSceneAudio.Play();
        endingPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetScore(int _playerScore)
    {
        if (_playerScore > highScore)
        {
            highScore = _playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            SetHighScore(highScore);
        }
        SetNowScore(_playerScore);
    }

    private void SetNowScore(int _score)
    {
        int[] indexArray = new int[5];
        indexArray[0] = _score % 10;
        indexArray[1] = (_score / 10) % 10;
        indexArray[2] = (_score / 100) % 10;
        indexArray[3] = (_score / 1000) % 10;
        indexArray[4] = (_score / 10000);

        for (int i = 0; i < 5; i++)
            nowScoreArray[i].sprite = numberSpriteArray[indexArray[i]];
    }

    private void SetHighScore(int _score)
    {
        int[] indexArray = new int[5];
        indexArray[0] = _score % 10;
        indexArray[1] = (_score / 10) % 10;
        indexArray[2] = (_score / 100) % 10;
        indexArray[3] = (_score / 1000) % 10;
        indexArray[4] = (_score / 10000);

        for (int i = 0; i < 5; i++)
            highScoreArray[i].sprite = numberSpriteArray[indexArray[i]];
    }

    IEnumerator MakeObstacle()
    {
        yield return new WaitForSeconds(regenStartTime);
        while (true)
        {
            float regenTime = Random.Range(regenIntervalTime[0], regenIntervalTime[1])/10f;
            yield return new WaitForSeconds(regenTime);
            int randomNum = Random.Range(0, obstacleArray.Length);
            GameObject tempObj = Instantiate(obstacleArray[randomNum]);
            tempObj.transform.SetParent(obstacleContainer);
        }
    }
}
