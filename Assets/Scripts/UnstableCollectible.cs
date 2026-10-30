using UnityEngine;

public class UnstableCollectible : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] float speedBonus = 5f;
    [SerializeField] float effectDuration = 5f;

    bool collected;

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            PlanetMovement movement = other.GetComponent<PlanetMovement>();

            if (movement == null)
            {
                movement = other.GetComponentInParent<PlanetMovement>();
            }

            if (movement != null)
            {
                movement.ActivateUnstableEffect(speedBonus, effectDuration);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("PlanetMovement introuvable sur le joueur !");
                collected = false;
            }
        }
    }
}