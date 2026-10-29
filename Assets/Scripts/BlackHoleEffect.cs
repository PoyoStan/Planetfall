using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(Camera))]
public class BlackHoleEffect : MonoBehaviour
{
    [SerializeField] public Shader shader;
    [SerializeField] public Transform blackHole;
    [SerializeField] public float ratio;
    [SerializeField] public float radius;

    Camera cam;
    Material _material;

    Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }
    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        ratio = 1f / cam.aspect;
    }
    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }
    Vector3 wtsp;
    Vector2 pos;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader && _material && blackHole)
        {
            Vector3 wtsp = cam.WorldToScreenPoint(blackHole.position);

            if (wtsp.z > 0)
            {
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);
                _material.SetVector("_Position", pos);
                _material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Rad", radius);
                _material.SetFloat("_Distance", Vector3.Distance(blackHole.position, transform.position));

                Graphics.Blit(source, destination, material);
            }
        }
    }
}