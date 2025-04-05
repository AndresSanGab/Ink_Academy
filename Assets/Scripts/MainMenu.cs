using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Cambia "Game" por el nombre de tu escena principal
        SceneManager.LoadScene("Game");
    }
}
