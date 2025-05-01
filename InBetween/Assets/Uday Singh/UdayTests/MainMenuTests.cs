using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class MainMenuTests
{
    private GameObject menuGO;
    private MainMenu mainMenu;

    [SetUp]
    public void SetUp()
    {
        // Create main menu GameObject and attach script
        menuGO = new GameObject("MainMenu");
        mainMenu = menuGO.AddComponent<MainMenu>();

        // Mock UI elements
        mainMenu.musicToggle = menuGO.AddComponent<Toggle>();

        mainMenu.backgroundMusic = menuGO.AddComponent<AudioSource>();
        mainMenu.musicVolumeSlider = menuGO.AddComponent<Slider>();
        mainMenu.sfxVolumeSlider = menuGO.AddComponent<Slider>();
        mainMenu.sfxAudioSource = menuGO.AddComponent<AudioSource>();

        GameObject settingsPanel = new GameObject("SettingsPanel");
        settingsPanel.AddComponent<RectTransform>(); // Dummy UI component
        mainMenu.settingsPanel = settingsPanel;

        // Dropdown setup
        GameObject dropdownGO = new GameObject("DifficultyDropdown");
        var tmpDropdown = dropdownGO.AddComponent<TMP_Dropdown>();
        tmpDropdown.options = new System.Collections.Generic.List<TMP_Dropdown.OptionData>()
        {
            new TMP_Dropdown.OptionData("Easy"),
            new TMP_Dropdown.OptionData("Medium"),
            new TMP_Dropdown.OptionData("Hard")
        };
        mainMenu.difficultyDropdown = tmpDropdown;
    }

    [UnityTest]
    public IEnumerator TestMusicToggleOffMutesAudio()
    {
        mainMenu.backgroundMusic.mute = false;
        mainMenu.backgroundMusic.Play();

        mainMenu.OnMusicToggleChanged(false);

        yield return null;

        Assert.IsTrue(mainMenu.backgroundMusic.mute);
        Assert.AreEqual(0, PlayerPrefs.GetInt("MusicOn"));
    }

    [UnityTest]
    public IEnumerator TestMusicToggleOnUnmutesAudio()
    {
        mainMenu.backgroundMusic.mute = true;
        mainMenu.OnMusicToggleChanged(true);

        yield return null;

        Assert.IsFalse(mainMenu.backgroundMusic.mute);
        Assert.AreEqual(1, PlayerPrefs.GetInt("MusicOn"));
    }

    [UnityTest]
    public IEnumerator TestMusicVolumeSliderChangesVolume()
    {
        float newVolume = 0.42f;
        mainMenu.OnMusicVolumeChanged(newVolume);

        yield return null;

        Assert.AreEqual(newVolume, mainMenu.backgroundMusic.volume);
        Assert.AreEqual(newVolume, PlayerPrefs.GetFloat("MusicVolume"));
    }

    [UnityTest]
    public IEnumerator TestSfxVolumeSliderChangesVolume()
    {
        float newVolume = 0.69f;
        mainMenu.OnSfxVolumeChanged(newVolume);

        yield return null;

        Assert.AreEqual(newVolume, mainMenu.sfxAudioSource.volume);
        Assert.AreEqual(newVolume, PlayerPrefs.GetFloat("SfxVolume"));
    }

    [UnityTest]
    public IEnumerator TestDifficultyDropdownChange()
    {
        mainMenu.OnDifficultyChanged(2); // "Hard"
        yield return null;

        Assert.AreEqual(2, PlayerPrefs.GetInt("Difficulty"));
    }

    [UnityTest]
    public IEnumerator TestSettingsPanelToggle()
    {
        mainMenu.settingsPanel.SetActive(false);
        mainMenu.OnSettingsButtonClicked();
        yield return null;

        Assert.IsTrue(mainMenu.settingsPanel.activeSelf);

        mainMenu.OnCloseSettingsClicked();
        yield return null;

        Assert.IsFalse(mainMenu.settingsPanel.activeSelf);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(menuGO);
        PlayerPrefs.DeleteAll();
    }
}
