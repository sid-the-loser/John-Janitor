using System;
using Sid.Scripts.Common;
using UnityEngine;
using FMOD.Studio;
using Sound.Scripts;
using Sound.Scripts.Sound; //sound

namespace Sid.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private float jumpVelocity = 4.5f;
        [SerializeField] private float walkingSpeed = 5.0f;
        [SerializeField] private float sprintingSpeed = 8.0f;
        [SerializeField] private float crouchingSpeed = 3.0f;
        [SerializeField] private float mouseSensitivity = 0.4f;
        [SerializeField] private float lerpSpeed = 10.0f;
        [SerializeField] private float crouchCameraY = -0.25f;
        [SerializeField] private float crouchColliderHeight = 1.5f;
        [SerializeField] private float crouchColliderY = -0.25f;
        // [SerializeField] private float gravity = -9.8f; // No need since we using a rigidbody
        [SerializeField] private LayerMask groundMask;
        
        private float _currentSpeed = 5.0f;
        private Vector3 _direction = Vector3.zero;
        private Vector3 _inputDirection = Vector3.zero;
        private bool _headWillCollide = false;
        private bool _canMove = false;
        private bool _grounded = false;
        private Vector3 _currentVel = Vector3.zero;
        private float _headRotationX = 0f;
        private float _headRotationY = 0f;

        private KeyCode _crouchKey = KeyCode.LeftControl;
        
        private CapsuleCollider _playerCollisionShape;
        private Rigidbody _playerRigidbody;

        private EventInstance _playerWalk; // sound

        
        private void Start()
        {
            // getting all the components
            _playerCollisionShape = GetComponent<CapsuleCollider>();
            _playerRigidbody = GetComponent<Rigidbody>();
            
            // disabling capsule rendering to prevent mesh clipping the camera
            GetComponent<MeshRenderer>().enabled = false;
            
            // syncing head rotation
            _headRotationX = headObject.transform.rotation.x;
            _headRotationY = headObject.transform.rotation.y;

            if (Application.isEditor)
                // DEV LOG (2:00 am : 02-Oct-2024)
                // ------------------------------------------------------------------------------------------
                // This check had to be done because unity is made by a couple of toddlers with computers who
                // think disabling editor hotkeys when running in game mode is "dumb".
                // ------------------------------------------------------------------------------------------
                _crouchKey = KeyCode.C;

            _playerWalk = AudioManager.Instance.CreateEventInstance(FmodEvents.Instance.Walk);
        }

        
        private void Update()
        {
            if (!GlobalVariables.Paused)
            {
                // _playerRigidbody.isKinematic = false;
                // ToggleMouseCapture(true);
                
                
                // crouch and speed logic
                if (Input.GetKey(_crouchKey))
                {
                    _currentSpeed = crouchingSpeed;
                    // TODO: Add head lowering, collider lowering and head collision checks (maybe done in next release)
                    // TODO[UNRELATED_TO_THIS_SCRIPT]: Work on enemies, basic melee and projectile enemies
                }
                else if (!_headWillCollide)
                {
                    _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintingSpeed : walkingSpeed;
                }
                
                _currentVel = _playerRigidbody.velocity;
                
                if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                {
                    _currentVel.y = jumpVelocity;
                }
                
                UpdateInputDirectionWASD();
                _direction = Vector3.Lerp(_direction, (transform.rotation * _inputDirection).normalized, Time.deltaTime * lerpSpeed);

                if (_direction != Vector3.zero)
                {
                    _currentVel.x = _direction.x * _currentSpeed;
                    _currentVel.z = _direction.z * _currentSpeed;
                }
                else
                {
                    var tempY = _currentVel.y;
                    _currentVel = Vector3.MoveTowards(_currentVel, Vector3.zero, _currentSpeed);
                    _currentVel.y = tempY;
                }
                
                _playerRigidbody.velocity = _currentVel;

                // mouse logic
                _headRotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                _headRotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

                _headRotationX = Mathf.Clamp(_headRotationX, -89f, 89f);

                headObject.transform.localEulerAngles = new Vector3(_headRotationX, 0, 0);
                transform.localEulerAngles = new Vector3(0, _headRotationY, 0);
                
                
            }
            else
            {
                // _playerRigidbody.isKinematic = true;
                // ToggleMouseCapture(false);
            }
        }


        private bool IsGrounded()
        {
            return Physics.SphereCast(transform.position, 0.9f, Vector3.down, out RaycastHit hit, 0.5f, groundMask);
        }


        private void UpdateInputDirectionWASD()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _inputDirection.z = 1;
                UpdateSound();
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                _inputDirection.z = -1;
                UpdateSound();
            }
            else
            {
                _inputDirection.z = 0;
                UpdateSound();
            }

            if (Input.GetKey(KeyCode.D))
            {
                _inputDirection.x = 1;
                UpdateSound();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _inputDirection.x = -1;
                UpdateSound();
            }
            else
            {
                _inputDirection.x = 0;
                UpdateSound();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateSound() //sound
        {
            if((Mathf.Abs(_playerRigidbody.velocity.x) >= 0.5f || Mathf.Abs(_playerRigidbody.velocity.z) >= 0.5f) && IsGrounded())
            {
                PLAYBACK_STATE playbackState;
                _playerWalk.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    _playerWalk.start();
                }
            }
            else
            {
                _playerWalk.stop(STOP_MODE.IMMEDIATE);
            }
                
        }
    }
}
