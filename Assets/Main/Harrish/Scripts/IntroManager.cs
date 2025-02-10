using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    
    [SerializeField] GameObject MMenuPhase0;
    [SerializeField] GameObject MMenuPhase1;
    [SerializeField] Scene Level1;
    
    [SerializeField] Animator MMenuPhase0Animator;
    [SerializeField] Animator MMenuPhase1Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        MMenuPhase0.SetActive(true);
        MMenuPhase1.SetActive(false);
    }

    public void ChangeToPhase1()
    {
        StartCoroutine(TimedChangeToPhase1());
    }

    IEnumerator TimedChangeToPhase1()
    {
        yield return new WaitForSeconds(1f);
        
        MMenuPhase0.SetActive(false);
        MMenuPhase1.SetActive(true);
    }
    
    public void ChangeToPhase0()
    {
        StartCoroutine(TimedChangeToPhase0());
    }
    
    IEnumerator TimedChangeToPhase0()
    {
        yield return new WaitForSeconds(1f);
        
        MMenuPhase0.SetActive(true);
        MMenuPhase1.SetActive(false);
    }
    
    public void ChangeToLevel1()
    {
        StartCoroutine(TimedChangeToLevel1());
        
    }
    
    IEnumerator TimedChangeToLevel1()
    {
        yield return new WaitForSeconds(2.2f);
        
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
