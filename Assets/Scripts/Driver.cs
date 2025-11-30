using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float steerSpeed = 0.1f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 10f;
    [SerializeField] float boostSpeed = 1f;
    [SerializeField] float speed = 0.5f;
    [SerializeField] TMP_Text boostText;
    private float previousMoveSpeed;
    private float maxMoveSpeed = 10f;
    // Cache 2D physics components for better performance and correct API usage
    Rigidbody2D rb2d;
    Collider2D col2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        boostText.gameObject.SetActive(false);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boost"))
        {
            moveSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);
            Destroy(collision.gameObject);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       moveSpeed = speed;
       boostText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0f;
        float steer = 0f;

        //Change to accelerate to a maximum speed instead of a constant speed
        if (Keyboard.current.upArrowKey.isPressed)
        {
            //add acceleration effect
            move = previousMoveSpeed + acceleration * Time.deltaTime;
            move = Mathf.Min(move, maxMoveSpeed);
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            move = previousMoveSpeed - acceleration * Time.deltaTime;
            move = Mathf.Max(move, -maxMoveSpeed);
        }
        else
        {
            // Gradually decelerate to 0 when no keys are pressed
            if (previousMoveSpeed > 0)
            {
                move = previousMoveSpeed - deceleration * Time.deltaTime;
                move = Mathf.Max(move, 0f); // Don't go below 0
            }
            else if (previousMoveSpeed < 0)
            {
                move = previousMoveSpeed + deceleration * Time.deltaTime;
                move = Mathf.Min(move, 0f); // Don't go above 0
            }
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            steer = 1f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            steer = -1f;
        }

        previousMoveSpeed = move;

        move = move * moveSpeed * Time.deltaTime;
        steer = steer * steerSpeed * Time.deltaTime;
        transform.Translate(0, move, 0);
        transform.Rotate(0, 0, steer);
    }
}
