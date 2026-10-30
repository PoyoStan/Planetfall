using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlanetMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float lateralSpeed = 5f;
    [SerializeField] float startingSpeed = 5f;
    [SerializeField] float acceleration = 0.2f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float minimumSpeed = 2f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 7f;

    [SerializeField] PlanetMass planetMass;

    Rigidbody rb;
    float currentSpeed;

    bool isGrounded;
    bool jumpRequested;

    public float CurrentSpeed => currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startingSpeed;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (planetMass != null && planetMass.IsDead) return;

        currentSpeed = Mathf.Min(
            currentSpeed + acceleration * Time.fixedDeltaTime,
            maxSpeed
        );

        float horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            bool leftPressed = Keyboard.current.leftArrowKey.isPressed;
            bool rightPressed = Keyboard.current.rightArrowKey.isPressed;

            if (leftPressed && !rightPressed) horizontalInput = -1f;
            else if (rightPressed && !leftPressed) horizontalInput = 1f;
        }

        rb.linearVelocity = new Vector3(
            horizontalInput * lateralSpeed,
            rb.linearVelocity.y,
            currentSpeed
        );

        if (jumpRequested)
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            jumpRequested = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f && rb.linearVelocity.y <= 0.1f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void SlowDown(float amount)
    {
        currentSpeed = Mathf.Max(currentSpeed - amount, minimumSpeed);
    }
}