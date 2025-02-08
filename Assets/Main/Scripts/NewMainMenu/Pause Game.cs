using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;  // Pauses the game
            pauseMenuUI.SetActive(true);  // Show pause menu
        }
        else
        {
            Time.timeScale = 1;  // Resumes the game
            pauseMenuUI.SetActive(false); // Hide pause menu
        }
    }
}

