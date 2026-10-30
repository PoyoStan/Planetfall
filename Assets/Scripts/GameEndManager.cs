using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameEndManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject victoryPanel;

    [Header("Victory UI")]
    [SerializeField] TMP_Text impactScoreText;

    bool endSequenceStarted;

    void Start()
    {
        Time.timeScale = 1f;

        deathPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void ShowDeath()
    {
        if (endSequenceStarted) return;

        endSequenceStarted = true;

        deathPanel.SetActive(true);

        StartCoroutine(PauseAfterDelay());
    }

    public void ShowVictory(float impactScore)
    {
        if (endSequenceStarted) return;

        endSequenceStarted = true;


        string levelName = SceneManager.GetActiveScene().name;
        string scoreKey = "BestImpact_" + levelName;

        float previousBest = PlayerPrefs.GetFloat(scoreKey, 0f);

        if (impactScore > previousBest)
        {
            PlayerPrefs.SetFloat(scoreKey, impactScore);
            PlayerPrefs.Save();
        }


        victoryPanel.SetActive(true);
        impactScoreText.text = "IMPACT : " + impactScore.ToString("F0");

        StartCoroutine(PauseAfterDelay());
    }

    IEnumerator PauseAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}