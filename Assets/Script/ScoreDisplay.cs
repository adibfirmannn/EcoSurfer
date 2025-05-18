using UnityEngine;
using TMPro; // Hanya jika pakai TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
        }
    }
}
