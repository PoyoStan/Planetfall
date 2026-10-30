
using UnityEngine;
using TMPro;

public class GoldenResourceManager : MonoBehaviour
{
    const string ResourceKey = "GoldenResources";

    [Header("UI")]
    [SerializeField] TMP_Text resourceText;

    public int CurrentResources => PlayerPrefs.GetInt(ResourceKey, 0);

    void Start()
    {
        UpdateDisplay();
    }

    public void AddResources(int amount)
    {
        int newTotal = CurrentResources + amount;

        PlayerPrefs.SetInt(ResourceKey, newTotal);
        PlayerPrefs.Save();

        UpdateDisplay();

        Debug.Log("Ressources dorées : " + newTotal);
    }

    public bool SpendResources(int amount)
    {
        if (CurrentResources < amount)
            return false;

        int newTotal = CurrentResources - amount;

        PlayerPrefs.SetInt(ResourceKey, newTotal);
        PlayerPrefs.Save();

        UpdateDisplay();

        return true;
    }

    public void UpdateDisplay()
    {
        if (resourceText != null)
        {
            resourceText.text = CurrentResources.ToString();
        }
    }
}
