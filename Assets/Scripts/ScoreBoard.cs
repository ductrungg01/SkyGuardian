using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int score = 0;

    private TMP_Text scoreText;

    #region Singleton
    private static ScoreBoard instance;
    private ScoreBoard() { }
    public static ScoreBoard Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Start";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;

        scoreText.text = $"Score: {score}";
    }

    public int GetScore()
    {
        return this.score;
    }
}
