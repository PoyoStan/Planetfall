using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectionPanel;
    [SerializeField] GameObject skinSelectionPanel;
    [SerializeField] GameObject optionsPanel;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        skinSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ShowLevelSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
        skinSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ShowSkinSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        skinSelectionPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        skinSelectionPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}