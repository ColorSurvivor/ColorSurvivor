using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverActive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ActiveObject;
    void Start()
    {
        if (ActiveObject != null)
        {
            ActiveObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ActiveObject != null)
        {
            AudioManager.instance.PlayHoverSound();
            ActiveObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ActiveObject != null)
        {
            ActiveObject.SetActive(false);
        }
    }
}
