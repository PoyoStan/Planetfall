using TMPro;
using UnityEngine;

public class PlanetHUD : MonoBehaviour
{
    [SerializeField] PlanetMass planetMass;
    [SerializeField] PlanetMovement planetMovement;
    [SerializeField] TMP_Text hudText;

    void Update()
    {
        hudText.text = "Masse : " + planetMass.CurrentMass.ToString("F0") + "\nVitesse : " + planetMovement.CurrentSpeed.ToString("F1");
    }
}