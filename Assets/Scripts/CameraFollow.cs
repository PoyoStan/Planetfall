using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0f, 3f, -7f);
    [SerializeField] float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}