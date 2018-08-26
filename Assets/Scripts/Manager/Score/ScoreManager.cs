using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

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
    #endregion

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        _score = 0;
    }

    public void AddScore(int score)
    {
        _score += score;
    }
}