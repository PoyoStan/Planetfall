using UnityEngine;

public class BlackHoleHazard : MonoBehaviour
{
    [SerializeField] float speedLoss = 3f;

    void OnTriggerEnter(Collider other)
    {
        PlanetMass planetMass = other.GetComponent<PlanetMass>();
        PlanetMovement planetMovement = other.GetComponent<PlanetMovement>();

        if (planetMass != null && !planetMass.IsDead)
        {
            planetMass.TakeDamage();

            if (planetMovement != null && !planetMass.IsDead)
            {
                planetMovement.SlowDown(speedLoss);
            }
        }
    }
}