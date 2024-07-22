using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ShipDeathBackgroundController : MonoBehaviour
{
    SpriteRenderer backgroundImage;
    IEnumerator backgroundFadeCoroutine;
    const float FadeDuration = 2f;

    [SerializeField] AnimationCurve fadeInterpolation;

    void Awake()
    {
        backgroundImage = GetComponent<SpriteRenderer>();

        Player player = FindObjectOfType<Player>();
        player.DeathAction += OnPlayerDie;

        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += OnEnemyDie;
    }

    void OnEnemyDie()
    {
        backgroundImage.color = Color.white;
        FadeBackground();
    }

    void OnPlayerDie()
    {
        backgroundImage.color = Color.black;
        FadeBackground();
    }

    void FadeBackground()
    {
        if (backgroundFadeCoroutine != null)
        {
            StopCoroutine(backgroundFadeCoroutine);
        }

        backgroundFadeCoroutine = _FadeBackground();
        StartCoroutine(backgroundFadeCoroutine);
    }

    IEnumerator _FadeBackground()
    {
        yield return WaitForSeconds(1.5f);
        backgroundImage.enabled = true;

        float currentLerpTime = 0f;

        while (currentLerpTime < FadeDuration)
        {
            float t = fadeInterpolation.Evaluate(currentLerpTime / FadeDuration);

            Color c = backgroundImage.color;
            c.a = Mathf.Lerp(0f, 1f, t);
            backgroundImage.color = c;            

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
}