// PortalController.cs
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [Tooltip("Kategori sampah yang diterima oleh portal ini, misal: Plastik, Kertas, Organik")]
    public string acceptedCategory;

    private void OnTriggerEnter(Collider other)
    {
        // Ambil PlayerController
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        // Ambil item yang sedang dipegang
        Item item = player.GetCurrentItem();
        if (item == null) return;

        // Cek kategori
        if (item.category == acceptedCategory)
        {
            ScoreManager.Instance.AddScore(10);
            Debug.Log("Pembuangan benar: +10 poin");
        }
        else
        {
            ScoreManager.Instance.SubtractScore(5);
            Debug.Log("Pembuangan salah: -5 poin");
        }

        // Kosongkan tangan pemain
        player.ClearCurrentItem();
    }
}
