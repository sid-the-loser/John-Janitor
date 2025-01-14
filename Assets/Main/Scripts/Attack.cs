using System.Collections;
using Sound.Scripts.Sound;
using UnityEngine;


namespace Udey.Scripts
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        [SerializeField] private float attackCooldown = 2f;

        private float _nextAttackTime;
        private Vector3 _camPos;
        private Camera _camera;

        private RaycastHit _otherHit;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Time.time > _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_camera)
                    {
                        _camPos = _camera.transform.position;

                        if (Physics.Raycast(_camPos, _camera.transform.forward, out var hit, 10f))
                        {
                            _otherHit = hit;
                            if (hit.transform.CompareTag("Enemy"))
                            {
                                StartCoroutine(FlashColor(hit.transform.gameObject));
                                StartCoroutine(OnDestory());
                            }
                        }
                    }
                    StartCoroutine(FakeAnimation());
                    _nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        private IEnumerator FakeAnimation()
        {
            image.transform.Rotate(0, 0, 25); // Rotate image to the left by 45 degrees
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.Swing, this.transform.position);
            yield return new WaitForSeconds(0.2f); // Wait for 0.1 seconds
            image.transform.Rotate(0, 0, -25); // Rotate image back to normal
        }
        private IEnumerator FlashColor(GameObject hit)
        {
            hit.GetComponent<Renderer>().material.SetColor("_RimColor", Color.white);
            yield return new WaitForSeconds(0.1f);
            hit.GetComponent<Renderer>().material.SetColor("_RimColor", new Color32(128,0,0,0));
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator OnDestory()
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(_otherHit.transform.gameObject);
            MusicChangeTrigger.enemyCounter--;
        }
    }
}