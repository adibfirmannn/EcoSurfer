using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI References")]
    public GameObject gameOverCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Jangan di-destroy antar scene kalau mau persistent (optional)
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("gameOverCanvas belum diassign di Inspector!");
        }
    }

    public void ShowGameOverUI()
    {
        Debug.Log("ShowGameOverUI called");
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void OnPlayAgainButton()
    {
        Debug.Log("Play Again clicked");
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit clicked");
        SceneManager.LoadScene("GameMenu");
    }
}
