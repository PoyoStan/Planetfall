
using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] Collider trackCollider;
    [SerializeField] GameObject warningMarker;
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] PlanetMass playerMass;

    [Header("Warning Settings")]
    [SerializeField] float minDistance = 25f;
    [SerializeField] float maxDistance = 40f;
    [SerializeField] float warningDuration = 3f;

    [Header("Spawn Settings")]
    [SerializeField] float minWaitTime = 5f;
    [SerializeField] float maxWaitTime = 10f;
    [SerializeField] float edgeMargin = 5f;

    void Start()
    {
        warningMarker.SetActive(false);
        StartCoroutine(WarningLoop());
    }

    IEnumerator WarningLoop()
    {
        while (true)
        {
            // Attendre que le joueur atteigne une masse de 30
            if (playerMass.CurrentMass < 30f)
            {
                yield return null;
                continue;
            }

            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // Vérifier à nouveau après l'attente
            if (playerMass.CurrentMass >= 30f && PlaceWarning())
            {
                warningMarker.SetActive(true);

                SpawnAsteroid();

                yield return new WaitForSeconds(warningDuration);

                warningMarker.SetActive(false);
            }
        }
    }


    bool PlaceWarning()
    {
        Bounds trackBounds = trackCollider.bounds;

        float randomX = Random.Range(
            trackBounds.min.x + edgeMargin,
            trackBounds.max.x - edgeMargin
        );

        float randomDistance = Random.Range(minDistance, maxDistance);

        float targetZ = player.position.z + randomDistance;

        Vector3 rayOrigin = new Vector3(
            randomX,
            trackBounds.max.y + 100f,
            targetZ
        );

        Debug.Log("=== ASTEROID WARNING ===");
        Debug.Log("Player Z : " + player.position.z);
        Debug.Log("Warning Z calculé : " + targetZ);
        Debug.Log("Track min Z : " + trackBounds.min.z);
        Debug.Log("Track max Z : " + trackBounds.max.z);
        Debug.Log("Raycast origine : " + rayOrigin);

        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (trackCollider.Raycast(ray, out RaycastHit hit, 500f))
        {
            warningMarker.transform.position = hit.point + hit.normal * 0.05f;
            warningMarker.transform.rotation =
                Quaternion.FromToRotation(Vector3.up, hit.normal);

            Debug.Log("Warning Z réel : " + warningMarker.transform.position.z);
            Debug.Log("Surface trouvée : " + hit.point);

            return true;
        }

        Debug.LogWarning("Raycast impossible : aucune surface trouvée à Z = " + targetZ);

        return false;
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = warningMarker.transform.position + Vector3.up * 40f;

        GameObject asteroid = Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Rigidbody asteroidRb = asteroid.GetComponent<Rigidbody>();

        asteroidRb.useGravity = false;
        asteroidRb.isKinematic = false;

        asteroidRb.linearVelocity = Vector3.down * 27f;
    }
}