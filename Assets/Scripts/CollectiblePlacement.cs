using UnityEngine;

public class CollectiblePlacement : MonoBehaviour
{
    [SerializeField] float heightAboveTrack = 0.5f;
    [SerializeField] float raycastHeight = 10f;
    [SerializeField] LayerMask trackLayer = ~0;

    void Start()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastHeight * 2f, trackLayer))
        {
            transform.position = hit.point + Vector3.up * heightAboveTrack;
        }
        else
        {
            Debug.LogWarning("Aucune surface détectée sous " + gameObject.name);
        }
    }
}