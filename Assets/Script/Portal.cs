// Portal.cs
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Kategori sampah yang diterima oleh portal ini")]
    public string acceptedCategory;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        Item item = player.GetCurrentItem();
        if (item == null) return;

        if (item.category == acceptedCategory)
        {
            ScoreManager.Instance.AddScore(10);
            Debug.Log("Pembuangan benar via Portal: +10 poin");
        }
        else
        {
            ScoreManager.Instance.SubtractScore(5);
            Debug.Log("Pembuangan salah via Portal: -5 poin");
        }

        player.ClearCurrentItem();
    }
}
