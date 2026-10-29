using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlanetMovement : MonoBehaviour
{
    [SerializeField] float lateralSpeed = 5f;
    [SerializeField] float startingSpeed = 5f;
    [SerializeField] float acceleration = 0.2f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float minimumSpeed = 2f;
    [SerializeField] PlanetMass planetMass;

    Rigidbody rb;
    float currentSpeed;

    public float CurrentSpeed => currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startingSpeed;
    }

    void FixedUpdate()
    {
        if (planetMass != null && planetMass.IsDead) return;

        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);

        float horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            bool leftPressed = Keyboard.current.leftArrowKey.isPressed;
            bool rightPressed = Keyboard.current.rightArrowKey.isPressed;

            if (leftPressed && !rightPressed) horizontalInput = -1f;
            else if (rightPressed && !leftPressed) horizontalInput = 1f;
        }

        Vector3 movement = Vector3.right * horizontalInput * lateralSpeed + Vector3.forward * currentSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    public void SlowDown(float amount)
    {
        currentSpeed = Mathf.Max(currentSpeed - amount, minimumSpeed);
    }
}
