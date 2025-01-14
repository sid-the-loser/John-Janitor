using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sid.Scripts.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void Update()
        {
        
        }

        public void LoadTestLevel()
        {
            SceneManager.LoadScene("Test Scene");
        }

        public void QuitGameButton()
        {
            Application.Quit();
        }
    }
}
