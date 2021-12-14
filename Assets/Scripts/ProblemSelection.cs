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
                GameManager.Instance.DeactivateWall();
                GameManager.Instance.InstantiatePlayer();
                UIController.Instance.SetProblemText("Instantiate circle to the center of the screen.");

                string playerPos = "Circle Position: " + GameManager.Instance.PController.transform.position.ToString();
                UIController.Instance.SetInformationText(playerPos);
                break;
            case 2:
                GameManager.Instance.DeactivateWall();
                GameManager.Instance.AddForceToPlayer(true);
                UIController.Instance.SetProblemText("Give constant speed to the circle.");                
                break;
            case 3:
                GameManager.Instance.InstantiateWall();
                GameManager.Instance.AddForceToPlayer();
                UIController.Instance.SetProblemText("Give circle bounciness and create a walls.");
                break;
            case 4:
                GameManager.Instance.InstantiateWall();
                GameManager.Instance.PlayerMoveUsingKeyboard();
                UIController.Instance.SetProblemText("Move circle using keyboard input");
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            default:
                break;
        }
    }
}
