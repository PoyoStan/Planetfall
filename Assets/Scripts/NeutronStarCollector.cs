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
            collected = true;

            planetMass.AddMass(massReward);
            scoreManager.AddScore(scoreReward);
            resourceManager.AddResources(goldenReward);

            Destroy(gameObject);
        }
    }
}