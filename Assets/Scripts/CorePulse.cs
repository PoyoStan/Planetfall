using UnityEngine;

public class CorePulse : MonoBehaviour
{
    [SerializeField] float pulseSpeed = 2f;
    [SerializeField] float pulseAmount = 0.08f;

    Vector3 startingScale;

    void Start()
    {
        startingScale = transform.localScale;
    }

    void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = startingScale * pulse;
    }
}