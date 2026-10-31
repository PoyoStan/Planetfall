using UnityEngine;

public class MassBarrier : MonoBehaviour
{
    [Header("Mass Requirement")]
    [SerializeField] float requiredMass = 30f;

    [Header("Materials")]
    [SerializeField] Renderer barrierRenderer;
    [SerializeField] Material lockedMaterial;
    [SerializeField] Material unlockedMaterial;

    [Header("Destruction")]
    [SerializeField] GameObject breakParticlesPrefab;

    [Header("Break Sound")]
    [SerializeField] AudioClip breakSound;
    [SerializeField] float breakSoundVolume = 1f;

    bool isUnlocked;
    bool hasBeenHit;

    void Update()
    {
        // Vérification de la masse du joueur
        // La barrière change de couleur automatiquement
        // si sa masse augmente ou diminue.

        PlanetMass playerMass = FindFirstObjectByType<PlanetMass>();

        if (playerMass == null || playerMass.IsDead)
            return;

        bool canBreak = playerMass.CurrentMass >= requiredMass;

        if (canBreak != isUnlocked)
        {
            isUnlocked = canBreak;

            if (barrierRenderer != null)
            {
                barrierRenderer.sharedMaterial = isUnlocked
                    ? unlockedMaterial
                    : lockedMaterial;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasBeenHit) return;

        PlanetMass playerMass =
            collision.collider.GetComponentInParent<PlanetMass>();

        if (playerMass == null || playerMass.IsDead)
            return;

        hasBeenHit = true;

        // Masse insuffisante : le joueur meurt
        if (playerMass.CurrentMass < requiredMass)
        {
            playerMass.Die();
            return;
        }

        // Masse suffisante : destruction de la barrière
        if (breakParticlesPrefab != null)
        {
            Vector3 impactPosition = collision.GetContact(0).point;

            Instantiate(
                breakParticlesPrefab,
                impactPosition,
                Quaternion.identity
            );
        }

        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(
                breakSound,
                collision.GetContact(0).point,
                breakSoundVolume
            );
        }

        Destroy(gameObject);
    }
}