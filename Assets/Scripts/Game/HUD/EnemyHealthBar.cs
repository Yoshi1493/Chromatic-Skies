using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

public class EnemyHealthBar : HUDComponent<Enemy>
{
    float EnemyHealthPercent => (float)ship.shipData.CurrentHealth.Value / ship.shipData.MaxHealth.Value;

    Image healthBarImage;

    [SerializeField] AnimationCurve refillInterpolation;
    IEnumerator refillCoroutine;

    protected override void Awake()
    {
        ship = FindObjectOfType<Enemy>();

        healthBarImage = GetComponent<Image>();
        healthBarImage.color = ship.shipData.UIColour.value;

        ship.TakeDamageAction += OnEnemyTakeDamage;
        ship.LoseLifeAction += OnEnemyLoseLife;
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