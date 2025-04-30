using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreditsManager : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform creditsPanel;
    public float scrollSpeed = 20f;
    public float startDelay = 2f;
    public float endDelay = 3f;
    public string titleSceneName = "MainMenu";

    private bool isScrolling = false;

    void Start()
    {
        if (creditsPanel != null)
            StartCoroutine(BeginCredits());
    }

    IEnumerator BeginCredits()
    {
        yield return new WaitForSeconds(startDelay);

        isScrolling = true;

        float panelHeight = creditsPanel.rect.height;
        float startY = creditsPanel.anchoredPosition.y;
        float targetY = startY + panelHeight + Screen.height;

        while (creditsPanel.anchoredPosition.y < targetY)
        {
            creditsPanel.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(endDelay);

        SceneManager.LoadScene(titleSceneName);
    }

    void Update()
    {
        if (isScrolling && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}
