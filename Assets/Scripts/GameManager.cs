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
    [SerializeField] private GameObject rectanglePrefab = null;

    [Header("Other References")]
    [SerializeField] private Transform wallParent = null;
    [SerializeField] private GameObject tapArea = null;
    [SerializeField] private Transform rectangleParent = null;

    [Header("Point Setting")]
    [SerializeField] private int maxRectangleOnScreen = 0;

    public PlayerController PController { get; private set; }

    private List<Wall> activeWall = new List<Wall>();
    private List<Rectangle> activeRectangle = new List<Rectangle>();

    private int maxRectangle = 0;

    public void InstantiatePlayer()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
            PController = newPlayer.GetComponent<PlayerController>();
            PController.ResetAllConfiguration();
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

    #region Mouse Input
    public void PlayerMoveUsingMouse()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            InstantiatePlayer();
        }
        if (!tapArea.activeSelf)
        {
            tapArea.SetActive(true);
        }

        PController.ResetAllConfiguration();
        PController.SetToControllByMouse();
    }

    public void MoveToPoint(Vector3 point)
    {
        PController.MoveToPoint(point);
    }

    public void DeactiveTapArea()
    {
        tapArea.SetActive(false);
    }
    #endregion

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
        }
        else
        {
            foreach (Wall wall in activeWall)
            {
                wall.gameObject.SetActive(true);
            }
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

    #region Rectangle/Point Configuration
    public void InstantiateRectangle()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            InstantiatePlayer();
        }

        maxRectangle = Random.Range(1, maxRectangleOnScreen);

        if (activeRectangle.Count == 0 || activeRectangle.Count < maxRectangle)
        {
            for (int i = 0; i < Mathf.Abs(maxRectangle - activeRectangle.Count); i++)
            {
                GameObject newRectangleObj = Instantiate(rectanglePrefab, rectangleParent);
                Rectangle newRectangle = newRectangleObj.GetComponent<Rectangle>();
                activeRectangle.Add(newRectangle);
            }

            for (int i = 0; i < activeRectangle.Count; i++)
            {
                activeRectangle[i].transform.position = SetRandomRectanglePosition();
            }
        }
        else
        {
            DeactiveAllRectangle();
            for (int i = 0; i < maxRectangle; i++)
            {
                activeRectangle[i].transform.position = SetRandomRectanglePosition();
                activeRectangle[i].gameObject.SetActive(true);
            }
        }
    }

    private Vector2 SetRandomRectanglePosition()
    {
        Vector2 rectangleSize = rectanglePrefab.GetComponent<SpriteRenderer>().size / 0.5f;
        
        Vector2 maxPos = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 0.8f));
        Vector2 minPos = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0.2f));

        float randomXPos = Random.Range(minPos.x, maxPos.x);
        float randomYPos = Random.Range(minPos.y, maxPos.y);

        Vector2 randomPos = new Vector2(randomXPos, randomYPos);

        while (activeRectangle.Find(r => r.transform.position == (Vector3)randomPos))
        {
            randomXPos = Random.Range(minPos.x, maxPos.x);
            randomYPos = Random.Range(minPos.y, maxPos.y);

            randomPos = new Vector2(randomXPos, randomYPos);
        }

        return randomPos;
    }

    public void DeactiveAllRectangle()
    {
        if (activeRectangle.Count == 0)
        {
            return;
        }

        for (int i = 0; i < activeRectangle.Count; i++)
        {
            activeRectangle[i].gameObject.SetActive(false);
            Debug.Log("TES");
        }
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
