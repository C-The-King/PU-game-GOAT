using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    private int score = 0;
    private int totalPoints = 0;
    TMPro.TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
        scoreText = GetComponent<TMPro.TMP_Text>();
        ResetScore();
//        scoreText.text = "0";
    }

    public void ResetScore() {
        score = 0;
        scoreText.text = score.ToString();
        totalPoints = GameObject.FindGameObjectsWithTag("Point").Length;
    }

    public void AddScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }

    public int GetScore() {
        return score;
    }

    public float GetScorePercentage() {
        if (totalPoints == 0) {
            return 0f;
        }
        return (float)score / totalPoints;
    }

}
