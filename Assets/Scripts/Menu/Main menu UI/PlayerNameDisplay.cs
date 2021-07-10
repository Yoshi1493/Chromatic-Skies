using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] StringObject shipName;
    TextMeshProUGUI nameDisplay;

    void Awake()
    {
        nameDisplay = GetComponentInChildren<TextMeshProUGUI>();
        nameDisplay.text = shipName.value;
    }
}