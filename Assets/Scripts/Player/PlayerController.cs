using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    [SerializeField] private Transform arrow = null;

    [Header("Player Area Limit")]
    [SerializeField] private Vector2 maxPos = Vector2.zero;
    [SerializeField] private Vector2 minPos = Vector2.zero;

    private Rigidbody2D rb2d;
    private Vector2 movement = Vector2.zero;

    private bool isAuto = false;
    private bool isKeyControlled = false;
    private bool isMouseControlled = false;

    private float force = 250;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameManager.Instance.gameType == GameType.MainGame)
        {
            SetToControllByKeyboard();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameType == GameType.MainGame)
        {
            if (isKeyControlled && !GameManager.Instance.isPaused)
            {
                RotateArrow();
                MovementLimit();
            }
        }        
    }

    private void FixedUpdate()
    {
        if (isKeyControlled)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Move(horizontal, vertical);
        }
    }    

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rb2d.MovePosition(transform.position + (Vector3)movement);
    }

    private void RotateArrow()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, targetAngle - 90f));

        arrow.transform.rotation = Quaternion.RotateTowards(arrow.transform.rotation, targetRotation, 180f);
    }

    private void MovementLimit()
    {
        float xPos = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);

        transform.position = new Vector2(xPos, yPos);
    }

    #region Move Using Force
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

    public void MoveToPoint(Vector2 pointerPos)
    {
        if (isMouseControlled)
        {
            Vector2 direction = (Vector3)pointerPos - transform.position;

            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(direction.normalized * force);
        }
    }
    #endregion

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
