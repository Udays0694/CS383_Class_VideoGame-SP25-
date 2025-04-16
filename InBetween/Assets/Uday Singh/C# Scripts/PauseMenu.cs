using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;

    private bool isPaused = false;

    public static PauseMenu instance;

    void Awake()
    {
        Debug.Log("PauseMenu Awake in: " + SceneManager.GetActiveScene().name);

        // Singleton pattern to ensure only one PauseMenu persists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene load event to refresh references
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel != null)
            {
                if (pauseMenuPanel.activeSelf)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to reassign the UI references when a new scene is loaded
        if (pauseMenuPanel == null)
            pauseMenuPanel = GameObject.FindWithTag("PauseMenuPanel");
        if (settingsPanel == null)
            settingsPanel = GameObject.FindWithTag("SettingsPanel");

        Debug.Log("Scene Loaded: " + scene.name);
        Debug.Log("PauseMenuPanel Found: " + (pauseMenuPanel != null));
        Debug.Log("SettingsPanel Found: " + (settingsPanel != null));
    }

    public void PauseGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Load main menu
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Game Quit");
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
