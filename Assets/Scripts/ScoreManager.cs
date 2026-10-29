using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreDisplay;

    int score = 0;
    public int CurrentScore => score;
    void Start()
    {
        scoreDisplay.UpdateScore(score);
    }

    public void AddScore(int amount)
    {
        score += amount;

        scoreDisplay.UpdateScore(score);
    }
}