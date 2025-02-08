using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public GameObject questMenu;
    public TMP_Text titleText;
    public TMP_Text infoText;

    public void OpenQuestMenu()
    {
        questMenu.SetActive(true);
        titleText.text = quest.title;
        infoText.text = quest.description;

    }

    public void CloseQuestMenu()
    {
        questMenu.SetActive(false);
    }

}
