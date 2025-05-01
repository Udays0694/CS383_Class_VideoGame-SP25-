using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void Restart() {
        SceneManager.LoadScene("RoomGenTest");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene");

    }
}
