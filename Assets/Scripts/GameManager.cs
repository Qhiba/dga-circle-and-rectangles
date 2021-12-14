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

    [Header("Required Prefabs")]
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject wallPrefab = null;

    [Header("Other References")]
    [SerializeField] private Transform wallParent = null;

    public PlayerController PController { get; private set; }

    private List<Wall> activeWall = new List<Wall>();

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

    public void AddForceToPlayer(bool isReset = false)
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            InstantiatePlayer();
        }

        if (isReset)
        {
            PController.ResetAllConfiguration();
        }

        PlayerOutOfScreen();
        PController.GiveInitialForce();

        Debug.Log("Force Added");
    }

    public void PlayerMoveUsingKeyboard()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            InstantiatePlayer();
        }

        PController.ResetAllConfiguration();
        PController.SetToControllByKeyboard();
    }

    #region Wall Configuration
    public void InstantiateWall()
    {
        if (activeWall.Count < 4)
        {
            string[] wallName = { "top wall", "bottom wall", "right wall", "left wall" };
            foreach (string wall in wallName)
            {
                GameObject newWallObj = activeWall.Find(w => w.name == wall)?.gameObject;
                if (newWallObj == null)
                {
                    newWallObj = Instantiate(wallPrefab, wallParent);
                    Wall newWall = newWallObj.GetComponent<Wall>();
                    activeWall.Add(newWall);

                    newWall.name = wall;
                    newWall.SetTransform();                    
                }
            }

            Debug.Log("Walls Instantiated");
        }
        else
        {
            foreach (Wall wall in activeWall)
            {
                wall.gameObject.SetActive(true);
            }

            Debug.Log("Walls Actived");
        }
    }

    public void DeactivateWall()
    {
        if (activeWall.Count < 4 || activeWall.Find(w => !w.gameObject.activeSelf))
        {
            return;
        }

        foreach (Wall wall in activeWall)
        {
            wall.gameObject.SetActive(false);
        }

        Debug.Log("Walls Deactived");
    }
    #endregion

    private void PlayerOutOfScreen()
    {
        if (PController == null)
        {
            return;
        }

        Vector2 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 playerPos = PController.transform.position;

        if (playerPos.x > screenToWorld.x || playerPos.x < -screenToWorld.x || playerPos.y > screenToWorld.y || playerPos.y < -screenToWorld.y)
        {
            PController.ResetAllConfiguration();
        }
    }
}
