// ScoreManager.cs
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Gunakan API terbaru untuk mencari instance
                _instance = Object.FindFirstObjectByType<ScoreManager>();
                if (_instance == null)
                    Debug.LogError("ScoreManager instance not found in scene!");
            }
            return _instance;
        }
    }

    private int score = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);  // Optional: biarkan tetap hidup antar scene
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score: {score}");
    }

    public void SubtractScore(int value)
    {
        score -= value;
        Debug.Log($"Score: {score}");
    }

    public int GetScore()
    {
        return score;
    }
}
