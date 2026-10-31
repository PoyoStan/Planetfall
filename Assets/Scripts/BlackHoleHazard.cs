using UnityEngine;
using System.Collections;

public class BlackHoleHazard : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] float speedLoss = 3f;

    [Header("Absorption Effect")]
    [SerializeField] float fragmentSize = 0.12f;
    [SerializeField] float absorptionDuration = 0.5f;
    [SerializeField] float spawnRadius = 0.8f;

    void OnTriggerEnter(Collider other)
    {
        PlanetMass planetMass = other.GetComponent<PlanetMass>();
        PlanetMovement planetMovement = other.GetComponent<PlanetMovement>();

        if (planetMass != null && !planetMass.IsDead)
        {
            // Récupération du matériau du joueur
            MeshRenderer playerRenderer = other.GetComponentInChildren<MeshRenderer>();

            if (playerRenderer != null)
            {
                StartCoroutine(AbsorbFragments(
                    other.transform,
                    playerRenderer.sharedMaterial
                ));
            }

            // Dégâts existants
            planetMass.TakeDamage();

            // Ralentissement existant
            if (planetMovement != null && !planetMass.IsDead)
            {
                planetMovement.SlowDown(speedLoss);
            }
        }
    }

    IEnumerator AbsorbFragments(Transform player, Material playerMaterial)
    {
        int fragmentCount = Random.Range(3, 5);

        for (int i = 0; i < fragmentCount; i++)
        {
            // Création d'une petite sphère
            GameObject fragment = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Suppression de son collider
            Destroy(fragment.GetComponent<Collider>());

            // Application du matériau du joueur
            fragment.GetComponent<MeshRenderer>().sharedMaterial = playerMaterial;

            // Taille de la sphère
            fragment.transform.localScale = Vector3.one * fragmentSize;

            // Position de départ autour du joueur
            Vector3 startPosition = player.position +
                Random.insideUnitSphere * spawnRadius;

            fragment.transform.position = startPosition;

            // Animation indépendante
            StartCoroutine(MoveFragment(fragment));
        }

        yield return null;
    }

    IEnumerator MoveFragment(GameObject fragment)
    {
        float timer = 0f;
        Vector3 startPosition = fragment.transform.position;

        while (timer < absorptionDuration)
        {
            if (fragment == null)
                yield break;

            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / absorptionDuration);

            // Déplacement vers le centre du trou noir
            fragment.transform.position = Vector3.Lerp(
                startPosition,
                transform.position,
                progress
            );

            // Rétrécissement progressif
            fragment.transform.localScale = Vector3.Lerp(
                Vector3.one * fragmentSize,
                Vector3.zero,
                progress
            );

            yield return null;
        }

        if (fragment != null)
        {
            Destroy(fragment);
        }
    }
}