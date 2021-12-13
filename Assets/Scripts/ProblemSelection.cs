using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemSelection : MonoBehaviour
{
    public void SelectProblem(int problem)
    {
        switch (problem)
        {
            case 1:
                GameManager.Instance.InstantiatePlayer();
                UIController.Instance.SetProblemText("Instantiate circle to the center of the screen.");

                string playerPos = "Circle Position: " + GameManager.Instance.PController.transform.position.ToString();
                UIController.Instance.SetInformationText(playerPos);
                break;
            case 2:
                GameManager.Instance.AddForceToPlayer();
                UIController.Instance.SetProblemText("Give constant speed to the circle.");                
                break;
            default:
                break;
        }
    }
}
