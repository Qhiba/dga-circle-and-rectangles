using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movement = Vector2.zero;

    private bool isAuto = false;
    private bool isKeyControlled = false;
    private bool isMouseControlled = false;

    private float force = 250;
    private float speed = 5;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isAuto)
        {
            UIController.Instance.SetInformationText("Circle Speed: " + rb2d.velocity.magnitude.ToString());
        }
        if (isKeyControlled)
        {
            UIController.Instance.SetInformationText("Use Up, Down, Right, and Left Key to Move.");

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Move(horizontal, vertical);
        }
    }

    #region Set Player Controll Type
    public void SetToAuto()
    {
        isAuto = true;
        isKeyControlled = false;
        isMouseControlled = false;
    }

    public void SetToControllByKeyboard()
    {
        isKeyControlled = true;
        isAuto = false;
        isMouseControlled = false;
    }

    public void SetToControllByMouse()
    {
        isMouseControlled = true;
        isAuto = true;
        isKeyControlled = false;
    }
    #endregion
    
    public void GiveInitialForce()
    {
        if (rb2d.velocity.magnitude > 0)
        {
            return;
        }

        SetToAuto();

        int random = Random.Range(0, 10);
        float posX = random < 5 ? -1.0f : 1.0f; //Only straight left (-1) or left (1)
        float posY = Random.Range(-1.0f, 1.0f); //It's possible for player to move on straight line

        Vector2 direction = new Vector2(posX, posY).normalized;

        rb2d.AddForce(direction * force);
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rb2d.MovePosition(transform.position + (Vector3)movement);
    }

    public void ResetAllConfiguration()
    {
        transform.position = Vector2.zero;
        rb2d.velocity = Vector2.zero;
        movement = Vector2.zero;
        isAuto = false;
        isKeyControlled = false;
        isMouseControlled = false;
    }
}
