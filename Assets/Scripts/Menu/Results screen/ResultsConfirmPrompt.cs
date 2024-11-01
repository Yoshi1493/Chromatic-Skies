using UnityEngine;
using TMPro;

public class ResultsConfirmPrompt : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] TextMeshProUGUI confirmText;

    void Awake()
    {
        ResultsScreen resultsScreen = GetComponentInParent<ResultsScreen>();
        resultsScreen.ResultsFinishDisplayAction += () => enabled = true;

        confirmText.enabled = false;
    }

    void OnEnable()
    {
        confirmText.enabled = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            levelLoader.LoadScene(0);
        }
    }
}