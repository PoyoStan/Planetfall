using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SkinData
{
    public string skinName;
    public Material skinMaterial;
    public int goldenCost;
    public bool isUnlocked;
}

public class SkinSelectionManager : MonoBehaviour
{
    [Header("Skins")]
    [SerializeField] SkinData[] skins;

    [Header("Preview")]
    [SerializeField] Renderer previewRenderer;

    [Header("UI")]
    [SerializeField] TMP_Text unlockButtonText;
    [SerializeField] RawImage neutronStarImage;
    [SerializeField] Button unlockButton;
    [SerializeField] Button selectButton;
    [SerializeField] GoldenResourceDisplay resourceDisplay;

    [Header("Level")]
    [SerializeField] LevelSelectionManager levelSelectionManager;

    int currentSkinIndex = 0;

    void Start()
    {
        foreach (SkinData skin in skins)
        {
            string skinKey = "SkinUnlocked_" + skin.skinName;

            if (PlayerPrefs.HasKey(skinKey))
            {
                skin.isUnlocked = PlayerPrefs.GetInt(skinKey) == 1;
            }
            else
            {
                PlayerPrefs.SetInt(skinKey, skin.isUnlocked ? 1 : 0);
            }
        }

        PlayerPrefs.Save();

        UpdateSkinDisplay();
    }

    public void NextSkin()
    {
        currentSkinIndex++;

        if (currentSkinIndex >= skins.Length)
        {
            currentSkinIndex = 0;
        }

        UpdateSkinDisplay();
    }

    public void PreviousSkin()
    {
        currentSkinIndex--;

        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skins.Length - 1;
        }

        UpdateSkinDisplay();
    }

    void UpdateSkinDisplay()
    {
        SkinData skin = skins[currentSkinIndex];

        previewRenderer.material = skin.skinMaterial;

        if (skin.isUnlocked)
        {
            unlockButton.gameObject.SetActive(false);
            neutronStarImage.gameObject.SetActive(false);
            selectButton.interactable = true;
        }
        else
        {
            unlockButton.gameObject.SetActive(true);
            neutronStarImage.gameObject.SetActive(true);
            unlockButtonText.text = "DÉBLOQUER — " + skin.goldenCost;
            selectButton.interactable = false;
        }
    }
    public void SelectSkin()
    {
        SkinData skin = skins[currentSkinIndex];

        if (!skin.isUnlocked) return;

        string sceneName = levelSelectionManager.SelectedSceneName;

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Aucune scène n'est associée à ce niveau !");
            return;
        }

        GameSelectionData.SelectedSceneName = sceneName;
        GameSelectionData.SelectedSkinMaterial = skin.skinMaterial;
        GameSelectionData.SelectedSkinName = skin.skinName;

        SceneManager.LoadScene(sceneName);
    }


    public void UnlockCurrentSkin()
    {
        SkinData skin = skins[currentSkinIndex];

        if (skin.isUnlocked) return;

        int resources = PlayerPrefs.GetInt("GoldenResources", 0);

        if (resources < skin.goldenCost)
        {
            Debug.Log("Pas assez de ressources dorées !");
            return;
        }

        // Dépenser les ressources
        resources -= skin.goldenCost;

        PlayerPrefs.SetInt("GoldenResources", resources);

        // Sauvegarder le déblocage
        string skinKey = "SkinUnlocked_" + skin.skinName;
        PlayerPrefs.SetInt(skinKey, 1);

        PlayerPrefs.Save();

        skin.isUnlocked = true;

        resourceDisplay.UpdateDisplay();
        UpdateSkinDisplay();

        Debug.Log("Skin débloqué : " + skin.skinName);
    }
}