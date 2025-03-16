using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Common;
using Sound.Scripts.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingmenumanager : MonoBehaviour
{
    #region Veriables

    #region TextVer

    [SerializeField] private GameObject cardDropDownPrefab;
    [Header("Headers")] [SerializeField] private TMP_Text[] headers = new TMP_Text[3];

    [Header("Descriptions")] [SerializeField]
    private TMP_Text[] descriptions = new TMP_Text[3];

    private List<int> _option;
    private const string ErrorMessage = "error";

    #endregion

    [Header("Level Prefabs")] [SerializeField]
    private GameObject[] levelPrefabs;

    private Queue<Scene> loadedScenes = new Queue<Scene>();
    private int levelIndex = 1;

    #endregion

    private void Start()
    {
        foreach (var header in headers)
        {
            header.fontSize = 60;
        }

        foreach (var description in descriptions)
        {
            description.fontSize = 60;
        }

        UseOptions(GetRandomOption());
    }

    private void DeactivateCardDropDown()
    {
        cardDropDownPrefab.SetActive(false);
        // BasicMeleeEnemy.UpdateStats();
        GlobalVariables.Paused = false;
    }

    private void Update()
    {
        if (cardDropDownPrefab.activeSelf)
        {
            GlobalVariables.Paused = true;
        }
    }

    #region RNG

    private List<int> GetRandomOption()
    {
        _option = Enumerable.Range(0, 10).OrderBy(x => Random.value).Take(3).Distinct().ToList();
        return _option;
    }

    #endregion
    #region Text

    private void UseOptions(List<int> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            headers[i].text = GetHeaderText(options[i]);
            descriptions[i].text = GetDescText(options[i]);
        }
    }

    private string GetHeaderText(int option)
    {
        return option switch
        {
            0 => "The Enemies Feast",
            1 => "The Enemies Sharpen Their Claws",
            2 => "The Enemies Blood Boils",
            3 => "The Enemies Muscle Relax",
            4 => "The Enemies Become Nimble",
            5 => "The Enemies Arms Grow longer",
            6 => "Not Added Yet",
            7 => "The Enemies Grow Harder",
            8 => "The Enemies Become Agile",
            9 => "The Enemies Feel Lucky",
            10 => "The Enemies Aims Better",
            _ => ErrorMessage
        };
    }

    private string GetDescText(int option)
    {
        return option switch
        {
            0 => "Increase Enemy Health",
            1 => "Increase Enemy Damage",
            2 => "Increase Enemy Health Regeneration",
            3 => "Increase Enemy Attack Speed",
            4 => "Increase Enemy Movement Speed",
            5 => "Increase Enemy Attack Range",
            6 => "Not Added Yet",
            7 => "Increase Enemy Defence",
            8 => "Increase Enemy Dodge Chance",
            9 => "Increase Enemy Critical Chance",
            10 => "Increase Enemy Critical Damage",
            _ => ErrorMessage
        };
    }

    #endregion
    #region CardSelection

    public void SelectedCardLeft()
    {
        //AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
        ChangeStats(descriptions[0].text);
        DeactivateCardDropDown();
        levelIndex = GlobalVariables.NextLevelIndex;
        LoadNextLevel();
        DeactivateScene();
        //StartCoroutine(startDialogue());
    }

    public void SelectedCardMiddle()
    {
        //AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
        ChangeStats(descriptions[1].text);
        DeactivateCardDropDown();
        levelIndex = GlobalVariables.NextLevelIndex;
        LoadNextLevel();
        DeactivateScene();
        //StartCoroutine(startDialogue());
    }

    public void SelectedCardRight()
    {
        //AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
        ChangeStats(descriptions[2].text);
        DeactivateCardDropDown();
        levelIndex = GlobalVariables.NextLevelIndex;
        LoadNextLevel();
        DeactivateScene();
        //StartCoroutine(startDialogue());
    }

    private void ChangeStats(string pickedOption)
    {
        switch (pickedOption)
        {
            case "Increase Enemy Health":
                GlobalVariables.EnemyMaxHealth *= 1.10f;
                break;
            case "Increase Enemy Damage":
                GlobalVariables.EnemyBaseDamage *= 1.10f;
                break;
            case "Increase Enemy Health Regeneration":
                GlobalVariables.EnemyBaseHpRegen *= 1.05f;
                break;
            case "Increase Enemy Attack Speed":
                GlobalVariables.EnemyAttSpeed *= 1.05f;
                break;
            case "Increase Enemy Movement Speed":
                GlobalVariables.EnemyMoveSpeed *= 1.10f;
                break;
            case "Increase Enemy Attack Range":
                GlobalVariables.EnemyAttRange *= 1.15f;
                break;
            case "Not Added Yet":
                Debug.Log("Not Added Yet");
                break;
            case "Increase Enemy Defence":
                GlobalVariables.EnemyBaseDefense *= 1.10f;
                break;
            case "Increase Enemy Dodge Chance":
                GlobalVariables.EnemyDodgeChance += (GlobalVariables.EnemyDodgeChance * 0.05f);
                break;
            case "Increase Enemy Critical Chance":
                GlobalVariables.EnemyCritChance += (GlobalVariables.EnemyCritChance * 0.05f);
                break;
            case "Increase Enemy Critical Damage":
                GlobalVariables.EnemyCritDamage += (GlobalVariables.EnemyCritDamage * 0.02f);
                break;
        }
    }

    #endregion

    public void ShowCards()
    {
        StartCoroutine(TimedCard());
    }

    IEnumerator TimedCard()
    {
        yield return new WaitForSeconds(3f);

        cardDropDownPrefab.SetActive(true);
    }

    IEnumerator TimedChangeToTitlescreen()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(GlobalVariables.NextLevelIndex);
    }

    void LoadNextLevel()
    {
        Scene newScene = SceneManager.CreateScene("Level_" + levelIndex);
        SceneManager.SetActiveScene(newScene);
        Instantiate(levelPrefabs[RandomLevelGrabber()], Vector3.zero, Quaternion.identity);

        loadedScenes.Enqueue(newScene);
        if (loadedScenes.Count > 2) // Assuming you want to keep only a certain number of segments loaded
        {
            SceneManager.UnloadSceneAsync(loadedScenes.Dequeue());
        }

        GlobalVariables.NextLevelIndex++;
    }

    private int RandomLevelGrabber()
    {
        return Random.Range(0, levelPrefabs.Length);
    }
    
    public void DeactivateScene()
    {
        Scene scene = SceneManager.GetSceneByName("Level Transition Scene");
        if (scene.isLoaded)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}