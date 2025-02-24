using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // Reference to the black panel's Image component

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f; // Start fully opaque
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime; // Reduce opacity over time
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // Apply new alpha
            yield return null; // Wait for the next frame
        }
        fadeImage.gameObject.SetActive(false); // Disable after fading
    }
}
