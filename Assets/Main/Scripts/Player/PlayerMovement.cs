using System;
using System.Collections;
using UnityEngine;
using FMOD.Studio;
using Main.Scripts.Common;
using Main.Scripts.Sound;
using Sound.Scripts;
using Sound.Scripts.Sound; //sound

namespace Sid.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private float jumpVelocity = 4.5f;
        private float _walkingSpeed = 5.0f;
        // [SerializeField] private float sprintingSpeed = 8.0f;
        // [SerializeField] private float crouchingSpeed = 3.0f;
        
        [SerializeField] private float lerpSpeed = 10.0f;
        [SerializeField] private float crouchCameraY = -0.25f;
        [SerializeField] private float crouchColliderHeight = 1.5f;
        [SerializeField] private float crouchColliderY = -0.25f;
        // [SerializeField] private float gravity = -9.8f; // No need since we are using a rigidbody
        [SerializeField] private LayerMask groundMask;
        
        [SerializeField] private Animator animator;
        
        // private float _currentSpeed = 5.0f; // We don't need it fam, we ain't sprinting
        private Vector3 _direction = Vector3.zero;
        private Vector3 _inputDirection = Vector3.zero;
        private bool _headWillCollide = false;
        private bool _canMove = false;
        private bool _grounded = false;
        private Vector3 _currentVel = Vector3.zero;
        private float _headRotationX = 0f;
        private float _headRotationY = 0f;
        
        private bool _movementStunned = false;
        private bool _mouseMovementStunned = false;

        private KeyCode _crouchKey = KeyCode.LeftControl;
        
        private CapsuleCollider _playerCollisionShape;
        private Rigidbody _playerRigidbody;

        private EventInstance playerWalk; // sound
        
        public float mouseSensitivity = 0.4f;

        // INFO: This was here to prevent camera snap when loading the object.
        
        private void Awake()
        {
            StartCoroutine(StartingStun(1f));
        }
        
        private void Start()
        {
            // getting all the components
            _playerCollisionShape = GetComponent<CapsuleCollider>();
            _playerRigidbody = GetComponent<Rigidbody>();

            _walkingSpeed = GetComponent<StatsBehaviour>().GetSpeed();
            
            // disabling capsule rendering to prevent mesh clipping the camera
            GetComponent<MeshRenderer>().enabled = false;
            
            // syncing head rotation
            _headRotationX = headObject.transform.localEulerAngles.x;
            _headRotationY = transform.localEulerAngles.y;
            
            animator.SetBool("isJogging", false);

            if (Application.isEditor)
                // DEV LOG (2:00 am : 02-Oct-2024)
                // ------------------------------------------------------------------------------------------
                // This check had to be done because unity is made by a couple of toddlers with computers who
                // think disabling editor hotkeys when running in game mode is "dumb".
                // ------------------------------------------------------------------------------------------
                _crouchKey = KeyCode.C;
            
            // INFO: IDK what this is! It's throwing an error
            playerWalk = AudioManager.Instance.CreateEventInstance(FmodEvents.Instance.Walk);
        }

        
        private void Update()
        {
            if (!GlobalVariables.Paused)
            {
                // _playerRigidbody.isKinematic = false;
                // ToggleMouseCapture(true);
                
                
                // crouch and speed logic
                // disabled sprinting because it felt unnecessary
                /*if (Input.GetKey(_crouchKey))
                {
                    _currentSpeed = crouchingSpeed;
                    // TODO: Add head lowering, collider lowering and head collision checks (maybe done in next release)
                    // TODO[UNRELATED_TO_THIS_SCRIPT]: Work on enemies, basic melee and projectile enemies
                }
                else if (!_headWillCollide)
                {
                    _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintingSpeed : walkingSpeed;
                }*/
                
                _currentVel = _playerRigidbody.velocity;
                
                if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                {
                    _currentVel.y = jumpVelocity;
                }
                
                UpdateInputDirectionWASD();
                _direction = Vector3.Lerp(_direction, (transform.rotation * _inputDirection).normalized, Time.deltaTime * lerpSpeed);

                if (_inputDirection == Vector3.zero)
                {
                    animator.SetBool("isJogging", false);
                }
                else
                {
                    animator.SetBool("isJogging", true);
                }

                if (_direction != Vector3.zero)
                {
                    UpdateSound();

                    _currentVel.x = _direction.x * _walkingSpeed;
                    _currentVel.z = _direction.z * _walkingSpeed;
                }
                else
                {
                    var tempY = _currentVel.y;
                    _currentVel = Vector3.MoveTowards(_currentVel, Vector3.zero, _walkingSpeed);
                    _currentVel.y = tempY;
                }
                
                if (!_movementStunned)
                {
                    _playerRigidbody.velocity = _currentVel;
                }

                if (!_mouseMovementStunned)
                {
                    // mouse logic
                    _headRotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
                    _headRotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                    _headRotationX = Mathf.Clamp(_headRotationX, -89f, 89f);

                    headObject.transform.localEulerAngles = new Vector3(_headRotationX, 0, 0);
                    transform.localEulerAngles = new Vector3(0, _headRotationY, 0);
                }
                
                
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
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                _inputDirection.z = -1;
            }
            else
            {
                _inputDirection.z = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _inputDirection.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _inputDirection.x = -1;
            }
            else
            {
                _inputDirection.x = 0;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateSound() //sound
        {
            if((Mathf.Abs(_playerRigidbody.velocity.x) >= 0.15f || Mathf.Abs(_playerRigidbody.velocity.z) >= 0.15f))
            {
                PLAYBACK_STATE playbackState;
                playerWalk.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    playerWalk.start();
                }
            }
            else
            {
                playerWalk.stop(STOP_MODE.IMMEDIATE);
            }
                
        }

        private IEnumerator StartingStun(float delay)
        {
            _mouseMovementStunned = true;
            _movementStunned = true;
            yield return new WaitForSeconds(delay);
            _mouseMovementStunned = false;
            _movementStunned = false;
        }
    }
}
