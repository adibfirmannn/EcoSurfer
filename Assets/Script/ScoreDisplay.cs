using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI terbuangText;
    public TextMeshProUGUI totalText;

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
            terbuangText.text = "Terbuang: " + ScoreManager.Instance.GetTerbuang();
            totalText.text = "Total: " + ScoreManager.Instance.GetTotalScore();
        }
    }
}
