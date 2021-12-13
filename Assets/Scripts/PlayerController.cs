using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool isConstantlyMoving = false;
    private float force = 100;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isConstantlyMoving)
        {
            UIController.Instance.SetInformationText("Circle Speed: " + rb2d.velocity.magnitude.ToString());
        }
    }

    public void GiveInitialForce()
    {
        int random = Random.Range(0, 10);
        float posX = random < 5 ? -1.0f : 1.0f; //Only straight left (-1) or left (1)
        float posY = Random.Range(-1.0f, 1.0f); //It's possible for player to move on straight line

        Vector2 direction = new Vector2(posX, posY).normalized;

        rb2d.AddForce(direction * force);
        isConstantlyMoving = true;
    }

    public void ResetAllConfiguration()
    {
        transform.position = Vector2.zero;
        rb2d.velocity = Vector2.zero;
        isConstantlyMoving = false;
    }
}
