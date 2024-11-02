using UnityEngine;
using UnityEngine.EventSystems;

public class InputForcer : MonoBehaviour
{
    GameObject lastSelectedGameObject;

    void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (currentSelectedGameObject != null)
        {
            lastSelectedGameObject = currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }
    }
}