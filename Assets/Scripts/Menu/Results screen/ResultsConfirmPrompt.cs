using UnityEngine;
using UnityEngine.EventSystems;

public class ResultsConfirmPrompt : MonoBehaviour
{
    [SerializeField] GameObject confirmButton;

    void Awake()
    {
        ResultsScreen resultsScreen = GetComponentInParent<ResultsScreen>();
        resultsScreen.ResultsFinishDisplayAction += () => enabled = true;

        confirmButton.SetActive(false);
    }

    void OnEnable()
    {
        print(EventSystem.current.currentSelectedGameObject.name);
        confirmButton.SetActive(true);
    }
}