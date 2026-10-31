
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public Texture previewImage;
    public string sceneName;
}

public class LevelSelectionManager : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] LevelData[] levels;

    [Header("UI")]
    [SerializeField] TMP_Text levelNameText;
    [SerializeField] RawImage levelImage;
    [SerializeField] TMP_Text bestScoreText;

    int currentLevelIndex = 0;

    public string SelectedSceneName => levels[currentLevelIndex].sceneName;
    public int SelectedLevelIndex => currentLevelIndex;

    void Start()
    {
        UpdateLevelDisplay();
    }

    public void NextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Length)
        {
            currentLevelIndex = 0;
        }

        UpdateLevelDisplay();
    }

    public void PreviousLevel()
    {
        currentLevelIndex--;

        if (currentLevelIndex < 0)
        {
            currentLevelIndex = levels.Length - 1;
        }

        UpdateLevelDisplay();
    }

    void UpdateLevelDisplay()
    {
        LevelData level = levels[currentLevelIndex];

        levelNameText.text = level.levelName;
        levelImage.texture = level.previewImage;

        string levelName = level.sceneName;

        if (!string.IsNullOrEmpty(levelName))
        {
            float bestScore = PlayerPrefs.GetFloat("BestImpact_" + levelName, 0f);
            bestScoreText.text = "MEILLEUR IMPACT : " + bestScore.ToString("F0");
        }
        else
        {
            bestScoreText.text = "MEILLEUR IMPACT : —";
        }
    }
}
