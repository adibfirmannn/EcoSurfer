using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;   // ? tambahkan ini!
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    public TextMeshProUGUI textComponent;

    [Header("Isi Dialog")]
    [TextArea] public string[] lines;
    public float textSpeed = .03f;

    // (opsional) ganti nama scene dari Inspector kalau mau
    [Header("Scene Tujuan")]
    public string nextSceneName = "SampleScene";

    private int index;
    private bool isTyping;

    // ????????????????????????????????????????????????????????????????
    void Start()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    // ????????????????????????????????????????????????????????????????
    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    // ----------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        // 1) Skip animasi jika masih mengetik
        if (isTyping)
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
            isTyping = false;
            return;
        }

        // 2) Lanjut ke baris berikutnya (masih ada dialog)
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // 3) Dialog selesai ? langsung load scene berikutnya
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
