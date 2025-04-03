using Main.Scripts.Sound;
using Sound.Scripts.Sound;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts
{
    public class UISounds : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
        }
        
    }
}
