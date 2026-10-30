using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectionPanel;
    [SerializeField] GameObject skinSelectionPanel;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        skinSelectionPanel.SetActive(false);
    }

    public void ShowLevelSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
        skinSelectionPanel.SetActive(false);
    }

    public void ShowSkinSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        skinSelectionPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}