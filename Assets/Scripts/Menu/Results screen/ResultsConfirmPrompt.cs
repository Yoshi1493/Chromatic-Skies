using UnityEngine;
using TMPro;

public class ResultsConfirmPrompt : MonoBehaviour
{
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
            //to-do: replace
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}