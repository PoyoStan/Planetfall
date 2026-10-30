
using UnityEngine;
using TMPro;

public class GoldenResourceDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text resourceText;

    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int resources = PlayerPrefs.GetInt("GoldenResources", 0);
        resourceText.text = resources.ToString();
    }
}
