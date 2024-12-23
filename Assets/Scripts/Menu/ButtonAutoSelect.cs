using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonAutoSelect : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        print($"Cursor entered {name}");
    }
}