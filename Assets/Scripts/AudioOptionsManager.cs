using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    const string VolumeKey = "GameVolume";

    void Start()
    {
        // Récupération du volume sauvegardé
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        // Application du volume
        AudioListener.volume = savedVolume;

        // Mise à jour du curseur
        volumeSlider.value = savedVolume;

        // Détection des changements du curseur
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float newVolume)
    {
        // Application du nouveau volume
        AudioListener.volume = newVolume;

        // Sauvegarde
        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        }
    }
}