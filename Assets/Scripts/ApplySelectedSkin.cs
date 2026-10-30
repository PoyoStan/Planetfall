using UnityEngine;

public class ApplySelectedSkin : MonoBehaviour
{
    [SerializeField] Renderer planetRenderer;

    void Start()
    {
        if (GameSelectionData.SelectedSkinMaterial == null)
        {
            return;
        }

        planetRenderer.material = GameSelectionData.SelectedSkinMaterial;
    }
}