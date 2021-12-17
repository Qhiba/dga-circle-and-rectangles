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

    [SerializeField] private Text problemText = null;
    [SerializeField] private Text informationText = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text respawnTimeText = null;

    private int score;
    private bool isCountingTime;

    private void Start()
    {
        problemText.text = "Choose your Problems";
    }

    private void Update()
    {
        if (isCountingTime)
        {
            RectRespawnTime();
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
    #endregion

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
}
