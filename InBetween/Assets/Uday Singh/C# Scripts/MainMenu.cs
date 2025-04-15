using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;  // For TMP_Dropdown

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingsPanel;

    [Header("Audio Settings")]
    public Toggle musicToggle;
    public AudioSource backgroundMusic;  // Drag the background music AudioSource here
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public AudioSource sfxAudioSource;  // Drag the SFX AudioSource here

    [Header("Game Preferences")]
    public TMP_Dropdown difficultyDropdown;

    private void Awake()
    {
        // Uncomment this if we want music to persist across scenes
        /*
        if (FindObjectsOfType<AudioSource>().Length > 1)
        {
            Destroy(gameObject); // Avoid duplicate music
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        */
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }


        // Load saved music state
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        if (musicToggle != null)
        {
            musicToggle.isOn = isMusicOn;
        }
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isMusicOn;
            if (isMusicOn && !backgroundMusic.isPlaying)
            {
                backgroundMusic.Play(); // Auto-play music if enabled
            }
        }

        // Load saved volume levels
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedMusicVolume;
        }
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = savedMusicVolume;
        }

        float savedSfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = savedSfxVolume;
        }
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = savedSfxVolume;
        }

        // Load saved difficulty level
        int savedDifficulty = PlayerPrefs.GetInt("Difficulty", 1); // Default: Medium
        if (difficultyDropdown != null)
        {
            difficultyDropdown.value = savedDifficulty;
        }

        // Hide settings panel on start
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Debug.Log("START - Music Toggle state: " + isMusicOn);
        Debug.Log("START - backgroundMusic.isPlaying: " + backgroundMusic.isPlaying);
        Debug.Log("START - backgroundMusic.volume: " + backgroundMusic.volume);
        Debug.Log("START - backgroundMusic.mute: " + backgroundMusic.mute);

    }

    // Play Button
    public void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked! Loading scene index 1.");
        SceneManager.LoadScene(1); 
    }

    // Exit Button
    public void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    // Settings Button
    public void OnSettingsButtonClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            Debug.Log("Settings panel opened.");
        }
    }

    // Back Button in Settings Panel
    public void OnCloseSettingsClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("Settings panel closed.");
        }
    }

    // Called when music toggle changes
 public void OnMusicToggleChanged(bool isOn)
{
    if (backgroundMusic == null)
    {
        Debug.LogWarning("No AudioSource assigned!");
        return;
    }

    Debug.Log($"[MusicToggle] isOn: {isOn}");

    backgroundMusic.mute = !isOn;

    if (isOn)
    {
        // Resume or play music
        if (!backgroundMusic.isPlaying)
        {
            // First try to UnPause in case it was paused
            backgroundMusic.UnPause();
            Debug.Log("[MusicToggle] UnPaused music");

            // If still not playing, force Play (from beginning or last position)
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
                Debug.Log("[MusicToggle] Play() forced");
            }
        }
    }
    else
    {
        // Only pause the playback
        backgroundMusic.Pause();
        Debug.Log("[MusicToggle] Paused music");
    }

    PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    PlayerPrefs.Save();

    Debug.Log($"[MusicToggle] Final State — isPlaying: {backgroundMusic.isPlaying}, mute: {backgroundMusic.mute}");
}



    void Update()
{
    if (backgroundMusic != null)
    {
        Debug.Log("Update() - Playing: " + backgroundMusic.isPlaying + ", Mute: " + backgroundMusic.mute);
    }
}



    // Called when difficulty is changed
    public void OnDifficultyChanged(int index)
    {
        PlayerPrefs.SetInt("Difficulty", index);
        string difficultyName = difficultyDropdown.options[index].text;
        Debug.Log("Difficulty set to: " + difficultyName);
    }

    // Called when music volume is changed
    public void OnMusicVolumeChanged(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Debug.Log("Music Volume set to: " + volume);
    }

    // Called when SFX volume is changed
    public void OnSfxVolumeChanged(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("SfxVolume", volume);
        Debug.Log("SFX Volume set to: " + volume);
    }
}
