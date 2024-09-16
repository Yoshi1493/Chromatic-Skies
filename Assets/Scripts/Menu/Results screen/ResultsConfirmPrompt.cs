using UnityEngine;

public class ResultsConfirmPrompt : MonoBehaviour
{
    float timeSinceEnabled = 0f;

    void Awake()
    {
        ResultsScreen resultsScreen = GetComponentInParent<ResultsScreen>();
        resultsScreen.ResultsFinishDisplayAction += () => enabled = true;
    }

    void OnEnable()
    {
        timeSinceEnabled = 0f;
    }

    void Update()
    {
        timeSinceEnabled += Time.deltaTime;

        if (timeSinceEnabled > 1f)
        {
            if (Input.GetButtonDown("Shoot"))
            {
                //to-do: replace
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
}