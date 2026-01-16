using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Chargement de la scène du jeu
    public void PlayGame()
    {
        SceneManager.LoadScene("GalaxyBox1/Examplescene");
    }

    // Quitter l'application
    public void QuitGame()
    {
        Application.Quit();
    }
}
