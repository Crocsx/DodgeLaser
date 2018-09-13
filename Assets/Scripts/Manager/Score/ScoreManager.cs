using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    public delegate void onNewHighScore(int score);
    public event onNewHighScore OnNewHighScore;

    public static int SCORE_ENEMY_DEAD = 1;

    #region Variables
    public int score
    {
        get
        {
            return _score;
        }
    }
    private int _score;

    public int highScore
    {
        get
        {
            int hScore = 0;
            if (PlayerPrefs.HasKey("highscore"))
                hScore = PlayerPrefs.GetInt("highscore");
            return hScore;
        }
    }
    #endregion

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(transform.gameObject);
    }
    #endregion

    private void Start()
    {
        GameManager.instance.OnStartGame += OnStart;
        GameManager.instance.OnFinishGame += OnFinish;
        _score = 0;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    void OnStart()
    {
        _score = 0;
    }

    void OnFinish()
    {
        if(highScore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
            if(OnNewHighScore != null)
            {
                OnNewHighScore(score);
            }
        }
    }
}