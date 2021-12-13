using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject playerPrefab = null;

    public PlayerController PController { get; private set; }


    public void InstantiatePlayer()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
            PController = newPlayer.GetComponent<PlayerController>();
            PController.ResetAllConfiguration();
            Debug.Log("Player Instantiated");
        }
        else
        {
            PController.ResetAllConfiguration();
        }
    }

    public void AddForceToPlayer()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            InstantiatePlayer();
        }

        PController.ResetAllConfiguration();
        PController.GiveInitialForce();
        Debug.Log("Force Added");
    }
}
