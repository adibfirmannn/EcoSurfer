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
    public float scoreInterval = 0.5f;      // Waktu antar skor
    public int scorePerInterval = 1;        // Jumlah skor per interval
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
    // TAMBAHKAN ke existing ScoreManager.cs

    [Header("Score Effects - EcoSurfer Addition")]
    public GameObject scorePopupPrefab;
    public Transform popupParent;

    // Method untuk score popup effect
    void ShowScorePopup(int points)
    {
        if (scorePopupPrefab != null && popupParent != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, popupParent);

            // Set text dan warna berdasarkan positive/negative
            Text popupText = popup.GetComponent<Text>();
            if (popupText != null)
            {
                popupText.text = points > 0 ? "+" + points : points.ToString();
                popupText.color = points > 0 ? Color.green : Color.red;
            }

            // Animate popup (fade out dan move up)
            StartCoroutine(AnimatePopup(popup));
        }
    }

    System.Collections.IEnumerator AnimatePopup(GameObject popup)
    {
        float duration = 1f;
        float timer = 0f;

        Vector3 startPos = popup.transform.position;
        Vector3 endPos = startPos + Vector3.up * 50f;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = popup.AddComponent<CanvasGroup>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Move up
            popup.transform.position = Vector3.Lerp(startPos, endPos, progress);

            // Fade out
            canvasGroup.alpha = 1f - progress;

            yield return null;
        }

        Destroy(popup);
    }

    // UPDATE existing AddScore method untuk include popup
    // Tambahkan baris ini di akhir method AddScore yang sudah ada:
    /*
    // Show score popup effect
    if (points != 0)
    {
        ShowScorePopup(points);
    }
    */
}
