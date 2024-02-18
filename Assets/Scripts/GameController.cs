using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject scoreBoardGO;

    [SerializeField] private GameObject GameOverTitle;
    [SerializeField] private GameObject SuccessfulTitle;
    [SerializeField] private GameObject ReplayButton;
    [SerializeField] private GameObject YourPoint;
    [SerializeField] private GameObject ReloadLevelIn;

    [Header("Timeline")] [SerializeField] private PlayableDirector timeline;
    
    private bool isGameover = false;
    private bool isSuccessful = false;
    private float timeToReload = 0;

    private double levelTime = 0;

    #region Singleton
    private static GameController instance = null;
    private GameController() { }

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameOverTitle.SetActive(false);
        YourPoint.SetActive(false);
        ReloadLevelIn.SetActive(false);
        scoreBoardGO.SetActive(true);
    }

    private void Start()
    {
        levelTime = timeline.duration + 0.5f;
    }

    public void StartGameOver(float timeToReload)
    {
        isGameover = true;
        this.timeToReload = timeToReload;
        
        GameOverTitle.SetActive(true);
        YourPoint.SetActive(true);
        YourPoint.GetComponent<TMP_Text>().text = "Your point: " + ScoreBoard.Instance.GetScore().ToString();

        ReloadLevelIn.SetActive(true);
        scoreBoardGO.SetActive(false);
    }

    private void Update()
    {
        if (isGameover)
        {
            ReloadLevelIn.GetComponent<TMP_Text>().text = "Reload level in " + (int)timeToReload + "s";
            timeToReload -= Time.deltaTime;
        }
        else
        {
            if (!isSuccessful)
            {
                levelTime -= Time.deltaTime;
                if (levelTime < 0)
                {
                    isSuccessful = true;
                    StartSuccessful();
                }
            }
        }
    }

    void StartSuccessful()
    {
        SuccessfulTitle.SetActive(true);
        YourPoint.SetActive(true);
        YourPoint.GetComponent<TMP_Text>().text = "Your point: " + ScoreBoard.Instance.GetScore().ToString();
        
        ReplayButton.SetActive(true);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReloadLevel);
    }

    public void ReloadLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex);
    }
}
