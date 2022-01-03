using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _instance = null;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIController>();
            }

            return _instance;
        }
    }

    [Header("Text Object")]
    [SerializeField] private Text problemText = null;
    [SerializeField] private Text informationText = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text timeText = null;
    [SerializeField] private Text respawnTimeText = null;
    [SerializeField] private Text finalScoreText = null;
    
    [Header("Panel Object")]
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject menuSelectionPanel = null;
    [SerializeField] private GameObject gameOverPanel = null;

    private int score;
    private bool isCountingTime;
    private float timer;

    private void Start()
    {
        ResetScore();
        timer = 0f;

        if (GameManager.Instance.gameType == GameType.ProblemSelection)
        {
            problemText.text = "Choose your Problems";
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameType == GameType.ProblemSelection)
        {
            if (isCountingTime)
            {
                RectRespawnTime();
            }
        }
        else if (GameManager.Instance.gameType == GameType.MainGame)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausePanel.SetActive(true);
                SceneController.Instance.PauseGame(true);
            }
        }
    }

    public void SetProblemText(string sentence)
    {
        problemText.text = sentence;
    }

    public void SetInformationText(string sentence)
    {
        informationText.text = sentence;
    }

    #region Score Configuration
    public void ActivateScoreText(bool isActive)
    {
        ResetScore();
        scoreText.gameObject.SetActive(isActive);
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = $"Your Score: {score.ToString()}";
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = $"Your Score: {score.ToString()}";
    }

    public void SetFinalScore()
    {
        finalScoreText.text = $"Score\t: {score.ToString()}";
    }
    #endregion

    #region Respawn Time Configuration
    public void RectRespawnTime()
    {
        float time = GameManager.Instance.GetCurrentRespawnTime();
        respawnTimeText.text = "Respawn Timer: " + time.ToString("0.00");
    }

    public void ActivateTimeText(bool isActive)
    {
        isCountingTime = isActive;
        respawnTimeText.gameObject.SetActive(isActive);
    }
    #endregion

    public void ActiveMenuSelectionPanel(bool isActive)
    {
        menuSelectionPanel.gameObject.SetActive(isActive);
    }

    public void ActiveGameOverPanel(bool isActive)
    {
        gameOverPanel.gameObject.SetActive(isActive);
        SetTimeText();
        SetFinalScore();
    }

    public void SetTimeText()
    {
        float minutes = timer / 60f;
        float seconds = timer % 60f;
        timeText.text = $"Time\t\t: {minutes.ToString("00")} : {seconds.ToString("00")}";
    }
}
