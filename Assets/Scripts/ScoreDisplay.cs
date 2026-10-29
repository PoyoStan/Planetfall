using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }
}