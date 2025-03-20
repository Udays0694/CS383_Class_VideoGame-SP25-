using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function starts the game by loading the main game scene
    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    // This function can be used to open settings (currently it just logs a message)
    public void OpenSettings()
    {
        Debug.Log("Settings Opened!"); 
        // Implement settings UI here (e.g., volume control)
    }

    // This function exits the game
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited!"); // Only works in a built application
    }
}
