using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    [SerializeField] Canvas loadingScreen;

    List<AsyncOperation> loadSceneOperations = new();
    IEnumerator loadSceneProgressCheck;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one (1) LoadingScreen in the scene.");
        }

        Instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.Menu, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        loadingScreen.enabled = true;

        loadSceneOperations.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Menu));
        loadSceneOperations.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Game, LoadSceneMode.Additive));

        if (loadSceneProgressCheck != null)
        {
            StopCoroutine(loadSceneProgressCheck);
        }

        loadSceneProgressCheck = GetSceneLoadProgress();
        StartCoroutine(loadSceneProgressCheck);
    }

    IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < loadSceneOperations.Count; i++)
        {
            while (!loadSceneOperations[i].isDone)
            {
                yield return null;
            }
        }

        loadingScreen.enabled = false;
        loadSceneProgressCheck = null;
    }
}