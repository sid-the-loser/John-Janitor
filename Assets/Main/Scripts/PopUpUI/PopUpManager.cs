using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private string objectivePrefix = "Current objective:";
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private float objPopUpDelay = 5.0f;
    [SerializeField] private float objPopUpAnimationFadeIn = 10.0f;
    [SerializeField] private float objPopUpAnimationFadeOut = 2.0f;
    
    private int _objPopUpCount = 0;

    private void Awake()
    {
        objectiveText.text = objectivePrefix;
    }

    private void Update()
    {
        objectiveText.color = 
            Color.Lerp(objectiveText.color, _objPopUpCount == 0 ? Color.clear : Color.red,
                (_objPopUpCount == 0 ? objPopUpAnimationFadeOut : objPopUpAnimationFadeIn) * Time.deltaTime);
    }

    public void SetObjective(string newObjective)
    {
        objectiveText.text = objectivePrefix + " " + newObjective;
        StartCoroutine(ObjectiveFadeStart());
    }

    private IEnumerator ObjectiveFadeStart()
    {
        _objPopUpCount++;
        yield return new WaitForSeconds(objPopUpDelay);
        _objPopUpCount--;
    }
}
