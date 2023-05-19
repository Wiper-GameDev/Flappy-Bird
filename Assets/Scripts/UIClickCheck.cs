using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIClickCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static List<int> clickElementsId = new List<int>();


    public static bool IsUIElementClicked()
    {
        return clickElementsId.Count > 0 || EventSystem.current.IsPointerOverGameObject();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clickElementsId.Add(gameObject.GetInstanceID());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clickElementsId.Remove(gameObject.GetInstanceID());
    }
}

