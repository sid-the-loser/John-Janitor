using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Common;
using Sid.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    [SerializeField] private Image dyingImage;
    [SerializeField] private Image almostDeadImage;
    [SerializeField] private TextMeshProUGUI enemyCount;
    [SerializeField] private GameObject _playerObject;

    private ElevatorBehaviour _elevator;

    private StatsBehaviour _playerStats;

    private PopUpManager _popUpManager;

    private bool _deathTriggered;

    public static bool _levelPassed;

    private float _pastHealth;
    private bool _firstDamageTick = true;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _playerStats = _playerObject.GetComponent<StatsBehaviour>();
        _popUpManager = GameObject.FindObjectOfType<PopUpManager>();
        _elevator = GameObject.FindObjectOfType<ElevatorBehaviour>();

        dyingImage.color = new Color(1f, 1f, 1f, 0f);
        _popUpManager.SetObjective("Clean up the garbage!");

        StartCoroutine(Level1PassedCheck());
    }

    private void Update()
    {
        if (_playerObject == null)
        {
            _playerObject = FindObjectOfType<PlayerMovement>().gameObject;
            _playerStats = _playerObject.GetComponent<StatsBehaviour>();
        }
        if (_playerObject.transform.position.y < -50)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }

        if (_playerStats.GetIsDead())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }

        if (_playerStats.GetHealth() != _pastHealth && !_firstDamageTick)
        {
            StartCoroutine(DamageEffect());
            _pastHealth = _playerStats.GetHealth();
        }
        else if (_firstDamageTick)
        {
            _firstDamageTick = false;
            _pastHealth = _playerStats.GetHealth();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _popUpManager.FlashObjective();
        }

        if (!_levelPassed)
        {
            StartCoroutine( Level1PassedCheck());
        }
        
        if (30.0f > (_playerStats.GetHealth() / _playerStats.GetMaxHealth())*100)
        {
            almostDeadImage.color = new Color(1f, 1f, 1f, 0.2f);
        }
    }

    private IEnumerator Level1PassedCheck()
    {
        var _count = GameObject.FindGameObjectsWithTag("Enemy").Length;

        enemyCount.text = $"Trash Count: {_count}";

        if (_count <= 0)
        {
            _popUpManager.SetObjective("Head back to the elevator.");
            _elevator.SetLastContact(true);
            _elevator.TriggerElevator();
            StartCoroutine(LevelTransition());
            _levelPassed = true;
        }

        yield return new WaitForSeconds(5f);
    }

    private IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Level Transition");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GlobalVariables.NextLevelIndex = 0;
        SceneManager.LoadScene(2);
    }

    private IEnumerator DamageEffect()
    {
        dyingImage.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        dyingImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
