using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    
    //variable to tell if the light is on or off
    public bool lightOn = false; 
    
    //variable for the delay between when the light is on or off
    public float flickerDelay;

    void Update()
    {
        if (lightOn == false) //light disabled as default
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        lightOn = true;
        this.gameObject.GetComponent<Light>().enabled = false; //actually enables or disables the light GameObject
        flickerDelay = UnityEngine.Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(flickerDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        flickerDelay = UnityEngine.Random.Range(0.01f, 1.2f);
        yield return new WaitForSeconds(flickerDelay);
        lightOn = false;

    }
}