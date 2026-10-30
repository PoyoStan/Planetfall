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
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Toutes les sauvegardes ont été réinitialisées.");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}