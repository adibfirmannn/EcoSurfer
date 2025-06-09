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
    private int terbuang = 0;

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
        Debug.Log($"Score: {score} | Terbuang: {terbuang} | Total: {GetTotalScore()}");
    }

    public void SubtractScore(int value)
    {
        score -= value;
        Debug.Log($"Score: {score} | Terbuang: {terbuang} | Total: {GetTotalScore()}");
    }

    public void AddTerbuang(int value)
    {
        terbuang += value;
        Debug.Log($"Terbuang ditambahkan: {value} | Total Terbuang: {terbuang} | Total: {GetTotalScore()}");
    }

    public int GetScore()
    {
        return score;
    }

    public int GetTerbuang()
    {
        return terbuang;
    }

    public int GetTotalScore()
    {
        return score + terbuang;
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
        terbuang = 0;
        timer = 0f;
    }
}
