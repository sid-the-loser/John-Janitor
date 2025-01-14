using Sid.Scripts.Common;
using Sound.Scripts.Sound;
using UnityEngine;

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
    }
}
