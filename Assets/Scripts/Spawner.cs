using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float delay = 1;

    [SerializeField] private GameObject rectanglePrefab = null;
    [SerializeField] private Transform[] spawners = null;
    [SerializeField] private Vector2 maxSpawnPos = Vector2.zero;
    [SerializeField] private Vector2 minSpawnPos = Vector2.zero;

    private List<Rectangle> activeRectangle = new List<Rectangle>();

    private bool isRespawning = false;

    private void Start()
    {
        isRespawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRespawning && !GameManager.Instance.isPaused)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    private void SpawnRectangle()
    {
        GameObject newRectangleObj = activeRectangle.Find(r => !r.gameObject.activeSelf)?.gameObject;
        if (newRectangleObj == null)
        {
            newRectangleObj = Instantiate(rectanglePrefab);
            activeRectangle.Add(newRectangleObj.GetComponent<Rectangle>());
        }

        newRectangleObj.transform.parent = SelectSpawner();
        newRectangleObj.transform.localPosition = (Vector2)SetRandomPos(newRectangleObj.transform.parent);
        newRectangleObj.SetActive(true);

        isRespawning = true;
    }

    private IEnumerator SpawnDelay()
    {
        float delayCount = delay;
        isRespawning = false;

        yield return new WaitForSecondsRealtime(delay);
        SpawnRectangle();
    }

    private Transform SelectSpawner()
    {
        int randomizeSpawnerLocation = Random.Range(0, spawners.Length);
        return spawners[randomizeSpawnerLocation];
    }

    private Vector2 SetRandomPos(Transform parent)
    {
        Vector2 spawnPos = Vector2.zero;
        string parentName = parent.gameObject.name;

        switch (parentName)
        {
            case "Top Spawner":
            case "Bottom Spawner":
                float randomXPos = Random.Range(minSpawnPos.x, maxSpawnPos.x);
                spawnPos = new Vector2(randomXPos, 0.0f);
                break;
            case "Right Spawner":
            case "Left Spawner":
                float randomYPos = Random.Range(minSpawnPos.y, maxSpawnPos.y);
                spawnPos = new Vector2(0.0f, randomYPos);
                break;
            default:
                break;
        }

        return spawnPos;
    }
}
