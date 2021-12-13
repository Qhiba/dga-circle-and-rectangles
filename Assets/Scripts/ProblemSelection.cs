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
                break;
            default:
                break;
        }
    }
}
