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

    [SerializeField] private Text problemText;
    [SerializeField] private Text informationText;

    private void Start()
    {
        problemText.text = "Choose your Problems";
    }

    public void SetProblemText(string sentence)
    {
        problemText.text = sentence;
    }

    public void SetInformationText(string sentence)
    {
        informationText.text = sentence;
    }
}
