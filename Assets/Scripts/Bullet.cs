using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector2 shootDirection;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void ShootAt(Vector2 direction, float force = 250)
    {

        shootDirection = direction.normalized;
        rb2d.AddForce(shootDirection * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }

        if (collision.transform.tag == "Enemy")
        {
            Rectangle rectangle = collision.GetComponent<Rectangle>();
            UIController.Instance.IncrementScore();

            rectangle.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
