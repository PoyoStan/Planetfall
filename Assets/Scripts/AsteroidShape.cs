using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class AsteroidShape : MonoBehaviour
{
    [SerializeField] float deformation = 0.2f;
    [SerializeField] float detail = 3f;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 direction = vertices[i].normalized;

            float noise = Mathf.PerlinNoise(
                direction.x * detail + 10f,
                direction.y * detail + 10f
            );

            float offset = (noise - 0.5f) * deformation;

            vertices[i] += direction * offset;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}