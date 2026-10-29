using UnityEngine;

public class PlanetCollector : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int scoreValue = 10;
    [SerializeField] PlanetMass planetMass;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            scoreManager.AddScore(scoreValue);
            planetMass.AbsorbDust();
            Destroy(other.gameObject);
        }
    }
}