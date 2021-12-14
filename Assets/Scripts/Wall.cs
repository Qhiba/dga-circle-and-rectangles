using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Vector2 wallPos;

    public void SetTransform()
    {
        switch (name)
        {
            case "top wall":
                wallPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.9f));
                transform.localScale = new Vector2(0.5f, 20.0f);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            case "bottom wall":
                wallPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f));
                transform.localScale = new Vector2(0.5f, 20.0f);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            case "right wall":
                wallPos = Camera.main.ViewportToWorldPoint(new Vector3(0.98f, 0.5f));
                transform.localScale = new Vector2(0.5f, 10.0f);
                break;
            case "left wall":
                wallPos = Camera.main.ViewportToWorldPoint(new Vector3(0.02f, 0.5f));
                transform.localScale = new Vector2(0.5f, 10.0f);
                break;
            default:
                break;
        }

        transform.position = wallPos;
    }
}
