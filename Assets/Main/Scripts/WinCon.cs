using System.Collections;
using Main.Scripts.Common;
using Sound.Scripts.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCon : MonoBehaviour
{
    public GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }
    public void Win()
    {
        GlobalVariables.Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winScreen.SetActive(true);
        StartCoroutine(ChangeDaScene(5));
    }

    private IEnumerator ChangeDaScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
}
