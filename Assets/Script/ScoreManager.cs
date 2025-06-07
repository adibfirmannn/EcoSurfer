using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindFirstObjectByType<ScoreManager>();
                if (_instance == null)
                    Debug.LogError("ScoreManager instance not found in scene!");
            }
            return _instance;
        }
    }

    private int score = 0;

    [Header("Auto Score Settings")]
    public bool isRunning = true;
    public float scoreInterval = 0.5f;      
    public int scorePerInterval = 1;       
    private float timer = 0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;
        if (timer >= scoreInterval)
        {
            AddScore(scorePerInterval);
            timer = 0f;
        }
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

    public void StopScoring()
    {
        isRunning = false;
    }

    public void ResumeScoring()
    {
        isRunning = true;
    }

    public void ResetScore()
    {
        score = 0;
        timer = 0f;
    }
   
}
