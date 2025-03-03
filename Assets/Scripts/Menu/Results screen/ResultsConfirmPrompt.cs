using UnityEngine;

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
        confirmButton.SetActive(true);
    }
}