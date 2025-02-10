using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingmenumanager : MonoBehaviour
{
   public void SceneChanger()
    {
       StartCoroutine(TimedChangeToTitlescreen());

    }
    IEnumerator TimedChangeToTitlescreen()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("TitleScreen");
    }

}
