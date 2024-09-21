using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    List<AsyncOperation> operations = new();

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
        SceneManager.UnloadSceneAsync((int)SceneIndexes.Menu);
        SceneManager.LoadSceneAsync((int)SceneIndexes.Game, LoadSceneMode.Additive);
    }
}