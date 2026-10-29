using UnityEngine;

public class OrbitRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}