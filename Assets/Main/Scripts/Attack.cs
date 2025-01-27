using System.Collections;
using FMOD;
using Main.Scripts;
using Main.Scripts.Player;
using Sound.Scripts.Sound;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Udey.Scripts
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        [SerializeField] private float meleeCoolDown = 0.75f;
        [SerializeField] private float meleeRange = 5f;

        private bool meleeOnCooldown = false;
        private Vector3 camPos;
        private new Camera camera;
        private LayerMask enemyLayer;
        private RaycastHit otherHit;

        private void Start()
        {
            camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && !meleeOnCooldown)
            {
                if (camera)
                {
                    camPos = camera.transform.position;

                    if (Physics.Raycast(camPos, camera.transform.forward, out var hit, 10f))
                    {
                        otherHit = hit;
                        if (hit.transform.CompareTag("Enemy"))
                        {
                            StartCoroutine(FlashColor(hit.transform.gameObject));
                            StartCoroutine(OnDestory());
                            StartCoroutine(MeleeCooldown());
                        }
                    }
                }
                StartCoroutine(FakeAnimation());
                StartCoroutine(MeleeCooldown());
            }
        }

        #region IEnumerator Members

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
            hit.GetComponent<Renderer>().material.SetColor("_RimColor", new Color32(128, 0, 0, 0));
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator OnDestory()
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(otherHit.transform.gameObject);
            Combo.ComboIncrease();
            MusicChangeTrigger.enemyCounter--;
        }

        private IEnumerator MeleeCooldown()
        {
            meleeOnCooldown = true;
            yield return new WaitForSeconds(meleeCoolDown);
            meleeOnCooldown = false;
        }

        #endregion
    }
}