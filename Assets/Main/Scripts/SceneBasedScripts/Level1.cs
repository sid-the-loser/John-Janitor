using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    [SerializeField] private Image dyingImage;
    
    private GameObject _playerObject;
    
    private ElevatorBehaviour _elevator;

    private StatsBehaviour _playerStats;
    
    private PopUpManager _popUpManager;
    
    private bool _deathTriggered;

    private bool _levelPassed;
    
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _playerObject = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
        _playerStats = _playerObject.GetComponent<StatsBehaviour>();
        _popUpManager = GameObject.FindObjectOfType<PopUpManager>();
        _elevator = GameObject.FindObjectOfType<ElevatorBehaviour>();
        
        dyingImage.color = new Color(1f, 1f, 1f, 0f);
        _popUpManager.SetObjective("Kill all enemies!");

        StartCoroutine(Level1PassedCheck());
    }

    private void Update()
    {
        if (_playerObject.transform.position.y < -50)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(1);
        }

        if (_playerStats.GetIsDead())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }

        var alpha = _playerStats.GetHealth() / _playerStats.GetMaxHealth();
        
        if (alpha is >= 0 and <= 1 && !_playerStats.GetIsDead())
        {
            dyingImage.color = new Color(1f, 1f, 1f, 1-alpha);
        }
        else
        {
            dyingImage.color = new Color(1f, 1f, 1f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _popUpManager.FlashObjective();
        }
    }

    private IEnumerator Level1PassedCheck()
    {
        while (!_levelPassed)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                _popUpManager.SetObjective("Head back to the elevator.");
                _elevator.SetLastContact(true);
                _elevator.TriggerElevator();
                _levelPassed = true;
            }
            
            yield return new WaitForSeconds(5f);   
        }
    }
}
