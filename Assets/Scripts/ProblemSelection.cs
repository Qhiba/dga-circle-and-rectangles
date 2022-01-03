using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemSelection : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameManager.Instance.PController != null)
        {
            if (GameManager.Instance.PController.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                UIController.Instance.SetInformationText("Circle Speed: " + GameManager.Instance.PController.GetComponent<Rigidbody2D>().velocity.magnitude.ToString());
            }
        }        
    }

    public void SelectProblem(int problem)
    {
        switch (problem)
        {
            case 1:
                SetOtherGameEnvironment();
                GameManager.Instance.InstantiatePlayer();
                UIController.Instance.SetProblemText("Instantiate circle to the center of the screen.");

                string playerPos = "Circle Position: " + GameManager.Instance.PController.transform.position.ToString();
                UIController.Instance.SetInformationText(playerPos);
                break;
            case 2:
                SetOtherGameEnvironment();
                GameManager.Instance.AddForceToPlayer(true);
                UIController.Instance.SetProblemText("Give constant speed to the circle.");                
                break;
            case 3:
                SetOtherGameEnvironment(true);
                GameManager.Instance.AddForceToPlayer();
                UIController.Instance.SetProblemText("Give circle bounciness and create a walls.");
                break;
            case 4:
                SetOtherGameEnvironment(true);
                GameManager.Instance.PlayerMoveUsingKeyboard();
                UIController.Instance.SetProblemText("Move circle using keyboard input");
                UIController.Instance.SetInformationText("Use Up, Down, Right, and Left Key to Move.");
                break;
            case 5:
                SetOtherGameEnvironment(true, true);
                GameManager.Instance.PlayerMoveUsingMouse();
                UIController.Instance.SetProblemText("Move circle to mouse clicked position (Click anywhere inside the border).");
                break;
            case 6:
                SetOtherGameEnvironment(true, false, true);
                GameManager.Instance.PlayerMoveUsingKeyboard();
                UIController.Instance.SetProblemText("Add Rectangle at random total and position (move circle with arrow key).");
                break;
            case 7:
                SetOtherGameEnvironment(true, false, true, true);
                UIController.Instance.ResetScore();
                GameManager.Instance.SetRectangleToDestrucable(true);
                GameManager.Instance.PlayerMoveUsingKeyboard();
                UIController.Instance.SetProblemText("Add score for every rectangle Player get.");
                break;
            case 8:
                SetOtherGameEnvironment(true, false, true, true, true);
                UIController.Instance.ResetScore();
                GameManager.Instance.SetRectangleToDestrucable(true);
                GameManager.Instance.SetRectangleRespawnable(true);
                GameManager.Instance.PlayerMoveUsingKeyboard();
                UIController.Instance.SetProblemText("Make Rectangle respawn after 3 seconds.");
                break;
            case 9:
                SetOtherGameEnvironment(false, false, false, false, false, true);
                GameManager.Instance.DeleteAll();
                UIController.Instance.SetProblemText("Go to Main Gameplay for Problem 9");
                break;
            default:
                break;
        }
    }

    //All game environment such as Walls, TapArea, RectanglePoint, Score UI, and other thing except Player/Circle
    private void SetOtherGameEnvironment(bool isWallActive = false, bool isTapAreaActive = false, bool isRectangleActive = false, bool isScoreActive = false, bool isRespawnActive = false, bool isMenuShown = false)
    {
        if (isWallActive)
        {
            GameManager.Instance.InstantiateWall();
        }
        else
        {
            GameManager.Instance.DeactivateWall();
        }

        if (!isTapAreaActive)
        {
            GameManager.Instance.DeactiveTapArea();
        }

        if (isRectangleActive)
        {
            GameManager.Instance.InstantiateRectangle();
        }
        else
        {
            GameManager.Instance.DeactiveAllRectangle();
        }
        
        UIController.Instance.ActivateScoreText(isScoreActive);
        UIController.Instance.ActivateTimeText(isRespawnActive);
        UIController.Instance.ActiveMenuSelectionPanel(isMenuShown);
    }
}
