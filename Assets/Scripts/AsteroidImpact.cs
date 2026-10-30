using UnityEngine;

public class AsteroidImpact : MonoBehaviour
{
    bool hasImpacted;

    void OnCollisionEnter(Collision collision)
    {
        if (hasImpacted) return;

        if (collision.gameObject.CompareTag("TrackSurface"))
        {
            hasImpacted = true;
            Destroy(gameObject);
            return;
        }

        PlanetMass planetMass = collision.gameObject.GetComponent<PlanetMass>();

        if (planetMass != null)
        {
            hasImpacted = true;
            planetMass.TakeDamage();
        }
    }
}