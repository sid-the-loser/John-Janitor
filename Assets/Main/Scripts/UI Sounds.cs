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
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
            eventData.selectedObject = gameObject;
        }
        
    }
}
