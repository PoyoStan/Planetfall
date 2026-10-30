
using UnityEngine;

public class FinalPlanetImpact : MonoBehaviour
{
    [SerializeField] float massWeight = 2f;
    [SerializeField] float speedWeight = 5f;
    [SerializeField] float scoreWeight = 0.1f;
    [SerializeField] float forceMultiplier = 10f;

    [Header("Victory")]
    [SerializeField] GameEndManager gameEndManager;

    bool impactResolved;

    void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRb = other.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            playerRb.useGravity = false;
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (impactResolved) return;

        PlanetMass planetMass = other.GetComponent<PlanetMass>();
        PlanetMovement planetMovement = other.GetComponent<PlanetMovement>();
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

        if (planetMass == null || planetMovement == null || scoreManager == null)
        {
            Debug.LogWarning("Impact ignoré : composant manquant.");
            return;
        }

        if (planetMass.IsDead) return;

        Rigidbody finalPlanetRb = GetComponent<Rigidbody>();

        if (finalPlanetRb == null)
        {
            Debug.LogError("FinalPlanet n'a pas de Rigidbody !");
            return;
        }

        impactResolved = true;

        float mass = planetMass.CurrentMass;
        float speed = planetMovement.CurrentSpeed;
        int score = scoreManager.CurrentScore;

        float impactPower = mass * massWeight + speed * speedWeight + score * scoreWeight;

        string result;

        if (impactPower < 50f)
            result = "Petit impact";
        else if (impactPower < 100f)
            result = "Bon impact";
        else
            result = "Impact colossal";

        Vector3 impactForce = Vector3.forward * impactPower * forceMultiplier;

        finalPlanetRb.isKinematic = false;
        finalPlanetRb.AddForce(impactForce, ForceMode.Impulse);

        Debug.Log("IMPACT FINAL !");
        Debug.Log("Masse : " + mass + " | Vitesse : " + speed + " m/s | Score : " + score);
        Debug.Log("Puissance d'impact : " + impactPower);
        Debug.Log("Force appliquée : " + impactForce.magnitude);
        Debug.Log("Résultat : " + result);

        if (gameEndManager != null)
        {
            gameEndManager.ShowVictory(impactPower);
        }
        else
        {
            Debug.LogWarning("GameEndManager non assigné !");
        }
    }
}
