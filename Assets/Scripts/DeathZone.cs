using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlanetMass planetMass = other.GetComponentInParent<PlanetMass>();

        if (planetMass != null)
        {
            planetMass.Die();
        }
    }
}