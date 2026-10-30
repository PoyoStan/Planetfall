using UnityEngine;
public class PlanetMass : MonoBehaviour
{
    [SerializeField] float startingMass = 10f;
    [SerializeField] float massPerDust = 1f;
    [SerializeField] float massLossPerHit = 5f;
    [SerializeField] float minimumSizeRatio = 0.5f;
    [SerializeField] GameEndManager gameEndManager;

    [Header("Death")]
    [SerializeField] GameObject explosionPrefab;

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
        float sizeMultiplier = Mathf.Pow(
            Mathf.Max(currentMass, startingMass * minimumSizeRatio) / startingMass,
            1f / 3f
        );

        transform.localScale = startingScale * sizeMultiplier;
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        currentMass = 0f;

        if (gameEndManager != null)
        {
            gameEndManager.ShowDeath();
        }

        if (explosionPrefab != null)
        {
            if (explosionPrefab != null)
            {
                float explosionScale = transform.localScale.x / startingScale.x;

                GameObject explosion = Instantiate(
                    explosionPrefab,
                    transform.position,
                    Quaternion.identity
                );

                explosion.transform.localScale =
                    explosionPrefab.transform.localScale * explosionScale;
            }
        }

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

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.enabled = false;
        }
    }
}