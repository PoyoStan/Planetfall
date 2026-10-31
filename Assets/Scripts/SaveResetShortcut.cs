using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SaveResetShortcut : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current == null) return;

        bool resetShortcut =
            Keyboard.current.leftCtrlKey.isPressed &&
            Keyboard.current.leftShiftKey.isPressed &&
            Keyboard.current.backspaceKey.wasPressedThisFrame;

        if (resetShortcut)
        {
            ResetAllSaves();
        }
    }

    void ResetAllSaves()
    {
        // Suppression de toutes les sauvegardes
        PlayerPrefs.DeleteAll();

        // Réinitialisation du volume à 100 %
        AudioListener.volume = 1f;

        // Enregistrement des modifications
        PlayerPrefs.Save();

        Debug.Log("Toutes les sauvegardes ont été réinitialisées.");

        // Rechargement de la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}