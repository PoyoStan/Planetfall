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

    [Header("Unstable Effect")]
    [SerializeField] ParticleSystem unstableEffectParticles;

    float temporarySpeedBonus;
    float unstableEffectTimer;
    bool reverseHorizontalControls;

    public float CurrentSpeed => currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startingSpeed;
    }

    void Update()
    {
        // Gestion de la durée de l'effet instable
        if (unstableEffectTimer > 0f)
        {
            unstableEffectTimer -= Time.deltaTime;

            if (unstableEffectTimer <= 0f)
            {
                EndUnstableEffect();
            }
        }

        if (Keyboard.current == null) return;

        // Gestion du saut
        if (Keyboard.current.spaceKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (planetMass != null && planetMass.IsDead) return;

        // Limite de vitesse temporairement augmentée
        float currentMaxSpeed = maxSpeed + temporarySpeedBonus;

        currentSpeed = Mathf.Min(
            currentSpeed + acceleration * Time.fixedDeltaTime,
            currentMaxSpeed
        );

        // Récupération des inputs latéraux
        float horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            bool leftPressed = Keyboard.current.leftArrowKey.isPressed;
            bool rightPressed = Keyboard.current.rightArrowKey.isPressed;

            if (leftPressed && !rightPressed) horizontalInput = -1f;
            else if (rightPressed && !leftPressed) horizontalInput = 1f;
        }

        // Inversion temporaire des commandes
        if (reverseHorizontalControls)
        {
            horizontalInput *= -1f;
        }

        // Déplacement du joueur
        rb.linearVelocity = new Vector3(
            horizontalInput * lateralSpeed,
            rb.linearVelocity.y,
            currentSpeed
        );

        // Gestion du saut
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
        currentSpeed = Mathf.Max(
            currentSpeed - amount,
            minimumSpeed
        );
    }

    // Activation de l'effet du cristal
    public void ActivateUnstableEffect(float speedBonus, float duration)
    {
        float bonusDifference = speedBonus - temporarySpeedBonus;

        currentSpeed += bonusDifference;
        temporarySpeedBonus = speedBonus;

        unstableEffectTimer = duration;
        reverseHorizontalControls = true;

        // Activation des particules
        if (unstableEffectParticles != null &&
            !unstableEffectParticles.isPlaying)
        {
            unstableEffectParticles.Play();
        }
    }

    // Fin de l'effet du cristal
    void EndUnstableEffect()
    {
        currentSpeed = Mathf.Max(
            currentSpeed - temporarySpeedBonus,
            minimumSpeed
        );

        temporarySpeedBonus = 0f;
        reverseHorizontalControls = false;
        unstableEffectTimer = 0f;

        // Arrêt des particules
        if (unstableEffectParticles != null)
        {
            unstableEffectParticles.Stop();
        }
    }
}