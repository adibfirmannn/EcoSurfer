using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    public ItemType acceptedCategory;
    public Color portalColor = Color.white;

    [Header("Visual Feedback")]
    public GameObject correctEffect;
    public GameObject wrongEffect;
    public float feedbackDuration = 1f;

    [Header("UI References")]
    public SpriteRenderer portalSprite;
    public TextMesh categoryLabel;

    private BoxCollider portalCollider;
    private bool isProcessing = false;

    void Start()
    {
        SetupPortal();
    }

    void SetupPortal()
    {
        // Setup collider
        portalCollider = GetComponent<BoxCollider>();
        if (portalCollider == null)
        {
            portalCollider = gameObject.AddComponent<BoxCollider>();
        }
        portalCollider.isTrigger = true;

        // Setup visual
        if (portalSprite != null)
        {
            portalSprite.color = portalColor;
        }

        // Setup label
        if (categoryLabel != null)
        {
            categoryLabel.text = acceptedCategory.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Cek apakah player yang masuk portal
        if (other.CompareTag("Player") && !isProcessing)
        {
            StartCoroutine(ProcessPlayerInventory());
        }
    }

    IEnumerator ProcessPlayerInventory()
    {
        isProcessing = true;

        InventoryManager inventory = InventoryManager.Instance;
        if (inventory == null)
        {
            isProcessing = false;
            yield break;
        }

        int correctItems = 0;
        int wrongItems = 0;
        int totalScore = 0;

        // Process semua item di inventory
        for (int i = 0; i < inventory.trashSlots.Length; i++)
        {
            TrashSlot slot = inventory.trashSlots[i];

            if (!slot.isEmpty)
            {
                if (slot.currentItem.itemType == acceptedCategory)
                {
                    // Item benar
                    correctItems++;
                    totalScore += slot.currentItem.pointValue;
                }
                else
                {
                    // Item salah
                    wrongItems++;
                    totalScore -= slot.currentItem.pointValue; // Minus point
                }

                // Clear slot
                inventory.RemoveItem(i);
            }
        }

        // Update score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(totalScore);
        }

        // Show feedback
        yield return StartCoroutine(ShowFeedback(correctItems, wrongItems, totalScore));

        isProcessing = false;
    }

    IEnumerator ShowFeedback(int correct, int wrong, int totalScore)
    {
        // Show visual feedback
        GameObject effectToShow = null;

        if (wrong == 0 && correct > 0)
        {
            // Perfect disposal
            effectToShow = correctEffect;
        }
        else if (wrong > 0)
        {
            // Ada item yang salah
            effectToShow = wrongEffect;
        }

        if (effectToShow != null)
        {
            GameObject effect = Instantiate(effectToShow, transform.position, Quaternion.identity);
            Destroy(effect, feedbackDuration);
        }

        // Show score popup (opsional)
        ShowScorePopup(totalScore);

        yield return new WaitForSeconds(feedbackDuration);
    }

    void ShowScorePopup(int score)
    {
        // Implementasi popup score
        // Bisa menggunakan UI Text yang animate
        Debug.Log($"Portal Score: {score}");
    }

    void OnDrawGizmosSelected()
    {
        // Visualisasi portal area
        Gizmos.color = portalColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}