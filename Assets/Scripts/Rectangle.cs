using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public bool IsDestructable { get; private set; }

    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 2.5f;

    private Transform target;

    private void OnEnable()
    {
        if (GameManager.Instance.gameType == GameType.MainGame)
        {
            SetDestructable(true);
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        }
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        if (GameManager.Instance.gameType == GameType.MainGame && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void SetDestructable(bool isDestructable)
    {
        IsDestructable = isDestructable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && IsDestructable)
        {
            Rectangle rectangle = GetComponent<Rectangle>();

            if (GameManager.Instance.gameType == GameType.MainGame)
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
            }
            else
            {
                UIController.Instance.IncrementScore();
                GameManager.Instance.AddToRectangleWaitingList(rectangle);
            }

            gameObject.SetActive(false);
        }
    }
}
