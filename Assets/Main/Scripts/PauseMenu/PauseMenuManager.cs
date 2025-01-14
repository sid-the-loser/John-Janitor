using System.Collections.Generic;
using Sid.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sid.Scripts.PauseMenu
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject resumeButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject mainMenuButton;

        private Rigidbody[] _allRigidBodies;
        private List<Vector3> _allVelocities;
        private bool _pastPaused;
    
        void Start()
        {
            UpdateRigidList();
            _pastPaused = !GlobalVariables.Paused;
        }
    
        void Update()
        {
            if (GlobalVariables.Paused)
            {
                if (_pastPaused != GlobalVariables.Paused)
                {
                    ToggleMouseCapture(false);
                    
                    for (int i = 0; i < _allRigidBodies.Length; i++)
                    {
                        _allVelocities[i] = _allRigidBodies[i].velocity;
                        _allRigidBodies[i].isKinematic = true;
                    }
                    
                    _pastPaused = !_pastPaused;
                }
            }
            else
            {
                if (_pastPaused != GlobalVariables.Paused)
                {
                    ToggleMouseCapture(true);
                    
                    for (int i = 0; i < _allRigidBodies.Length; i++)
                    {
                        _allRigidBodies[i].isKinematic = false;
                        _allRigidBodies[i].velocity = _allVelocities[i];
                    }
                    
                    _pastPaused = !_pastPaused;
                }
            }
            
            if (!Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) PauseToggle();
            }
            if (Input.GetKeyDown(KeyCode.P)) PauseToggle();
        }

        private void PauseToggle()
        {
            GlobalVariables.Paused = !GlobalVariables.Paused;

            if (GlobalVariables.Paused)
            {
                resumeButton.SetActive(true);
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
            }
            else
            {
                resumeButton.SetActive(false);
                restartButton.SetActive(false);
                mainMenuButton.SetActive(false);
            }
        }

        public void ReloadLevel()
        {
            GlobalVariables.Paused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ResumeLevel()
        {
            PauseToggle();
        }

        public void MainMenuLoad()
        {
            GlobalVariables.Paused = false;
            SceneManager.LoadScene("PlaceHolder");
        }
        
        private void UpdateRigidList()
        {
            _allRigidBodies = FindObjectsOfType<Rigidbody>(); // please delete this variable and everything that is related to it
            _allVelocities = new List<Vector3>();
            
            foreach (var rb in _allRigidBodies)
            {
                _allVelocities.Add(rb.velocity);
            }
        }
        
        private void ToggleMouseCapture(bool flag)
        {
            // cursor control
            Cursor.lockState = flag ? CursorLockMode.Confined : CursorLockMode.None;
            Cursor.visible = !flag;
        }
    }
}
