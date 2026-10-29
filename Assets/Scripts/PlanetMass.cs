using UnityEngine;

public class PlanetMass : MonoBehaviour
{
    [SerializeField] float startingMass = 10f;
    [SerializeField] float massPerDust = 1f;
    [SerializeField] float massLossPerHit = 5f;
    [SerializeField] float minimumSizeRatio = 0.5f;

    float currentMass;
    Vector3 startingScale;
    Rigidbody rb;

    public float CurrentMass => currentMass;
    public bool IsDead { get; private set; }

    void Awake()
    {
        currentMass = startingMass;
        startingScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        UpdateSize();
    }

    public void AddMass(float amount)
    {
        if (IsDead) return;

        currentMass += amount;
        UpdateSize();
    }

    public void AbsorbDust()
    {
        AddMass(massPerDust);
    }

    public void TakeDamage()
    {
        if (IsDead) return;

        currentMass = Mathf.Max(0f, currentMass - massLossPerHit);

        if (currentMass <= 0f)
        {
            Die();
            return;
        }

        UpdateSize();
    }

    void UpdateSize()
    {
        float sizeMultiplier = Mathf.Pow(Mathf.Max(currentMass, startingMass * minimumSizeRatio) / startingMass, 1f / 3f);
        transform.localScale = startingScale * sizeMultiplier;
    }

    void Die()
    {
        IsDead = true;
        currentMass = 0f;
        UpdateSize();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        PlanetMovement movement = GetComponent<PlanetMovement>();

        if (movement != null)
        {
            movement.enabled = false;
        }
    }
}
