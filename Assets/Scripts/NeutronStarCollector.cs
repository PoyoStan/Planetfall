using UnityEngine;

public class NeutronStarCollector : MonoBehaviour
{
    [Header("Collection")]
    [SerializeField] float massReward = 4f;
    [SerializeField] int goldenReward = 1;
    [SerializeField] int scoreReward = 40;

    [Header("References")]
    [SerializeField] PlanetMass planetMass;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GoldenResourceManager resourceManager;

    bool collected;

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            // Récupération des composants du joueur
            if (planetMass == null)
                planetMass = other.GetComponentInParent<PlanetMass>();

            if (scoreManager == null)
                scoreManager = FindFirstObjectByType<ScoreManager>();

            if (resourceManager == null)
                resourceManager = FindFirstObjectByType<GoldenResourceManager>();

            // Vérification des références
            if (planetMass == null ||
                scoreManager == null ||
                resourceManager == null)
            {
                Debug.LogWarning("Collecte impossible : référence manquante !");
                return;
            }

            collected = true;

            planetMass.AddMass(massReward);
            scoreManager.AddScore(scoreReward);
            resourceManager.AddResources(goldenReward);

            Destroy(gameObject);
        }
    }
}