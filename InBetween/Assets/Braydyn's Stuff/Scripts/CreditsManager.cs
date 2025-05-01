using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject creditsPanel;
    public float scrollSpeed = 20f;
    public float fastScrollMultiplier = 3f;
    public float startDelay = 2f;
    public float endDelay = 3f;
    public string titleSceneName = "MainMenu";

    private bool isScrolling = false;
    private float scrollOffset = 100f;

    private RectTransform creditsRect;

    void Start()
    {
        if (creditsPanel != null)
        {
            creditsRect = creditsPanel.GetComponent<RectTransform>();
            StartCoroutine(BeginCredits());
        }
    }

    IEnumerator BeginCredits()
    {
        yield return new WaitForSeconds(startDelay);

        isScrolling = true;

        float panelHeight = creditsRect.rect.height;
        float screenHeight = ((RectTransform)creditsRect.parent).rect.height;
        float totalScrollDistance = panelHeight + scrollOffset + screenHeight;

        float scrolled = 0f;

        while (scrolled < totalScrollDistance)
        {
            float currentSpeed = scrollSpeed;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                currentSpeed *= fastScrollMultiplier;

            float deltaY = currentSpeed * Time.deltaTime;
            scrolled += deltaY;

            creditsRect.anchoredPosition += new Vector2(0, deltaY);
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
    