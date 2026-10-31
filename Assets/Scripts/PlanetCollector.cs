using UnityEngine;

public class PlanetCollector : MonoBehaviour
{
    [Header("Collection")]
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int scoreValue = 10;
    [SerializeField] PlanetMass planetMass;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dustCollectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            scoreManager.AddScore(scoreValue);
            planetMass.AbsorbDust();

            // Son de collecte
            if (audioSource != null && dustCollectSound != null)
            {
                audioSource.PlayOneShot(dustCollectSound);
            }

            Destroy(other.gameObject);
        }
    }
}