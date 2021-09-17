using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

public class EnemyHealthBar : MonoBehaviour
{
    Image healthBarImage;

    [SerializeField] Enemy enemyShip;
    float EnemyHealthPercent => (float)enemyShip.shipData.CurrentHealth.Value / enemyShip.shipData.MaxHealth.Value;

    [SerializeField] AnimationCurve refillInterpolation;
    IEnumerator refillCoroutine;

    void Awake()
    {
        healthBarImage = GetComponent<Image>();
        healthBarImage.color = enemyShip.shipData.UIColour.value;

        enemyShip.TakeDamageAction += OnEnemyTakeDamage;
        enemyShip.LoseLifeAction += OnEnemyLoseLife;
    }

    void OnEnemyTakeDamage()
    {
        if (refillCoroutine == null)
            healthBarImage.fillAmount = EnemyHealthPercent;
    }

    void OnEnemyLoseLife()
    {
        if (refillCoroutine != null)
            StopCoroutine(refillCoroutine);

        refillCoroutine = Refill();
        StartCoroutine(refillCoroutine);
    }

    IEnumerator Refill()
    {
        healthBarImage.fillAmount = 0f;

        yield return WaitForSeconds(1f);

        float currentLerpTime = 0f;
        float totalLerpTime = 0.5f;

        while (currentLerpTime < totalLerpTime)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;
            healthBarImage.fillAmount = Mathf.Lerp(0f, EnemyHealthPercent, refillInterpolation.Evaluate(lerpProgress));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        refillCoroutine = null;
    }
}