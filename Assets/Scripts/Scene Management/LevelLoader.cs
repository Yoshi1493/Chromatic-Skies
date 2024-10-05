using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CoroutineHelper;

public class LevelLoader : MonoBehaviour
{
    Animator animator;

    IEnumerator sceneTransitionCoroutine;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(int sceneIndex)
    {
        if (sceneTransitionCoroutine != null)
        {
            StopCoroutine(sceneTransitionCoroutine);
        }

        sceneTransitionCoroutine = RunSceneTransition(sceneIndex);
        StartCoroutine(sceneTransitionCoroutine);
    }

    IEnumerator RunSceneTransition(int sceneIndex)
    {
        animator.SetTrigger("Start");

        yield return null;
        yield return WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        SceneManager.LoadScene(sceneIndex);
    }
}