using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Nom de la scène menu
    public string menuSceneName = "MainMenu";

    void Update()
    {
        // Si on appuie sur Echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Charger la scène du menu
            SceneManager.LoadScene(menuSceneName);

            // Optionnel : déverrouiller le curseur
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
