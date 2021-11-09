using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

public class HealthBar<TShip> : HUDComponent<TShip>
    where TShip : Ship
{
    Image healthBarImage;
    float HealthPercent => (float)ship.shipData.CurrentHealth.Value / ship.shipData.MaxHealth.Value;

    [SerializeField] AnimationCurve refillInterpolation;
    IEnumerator refillCoroutine;

    protected override void Awake()
    {
        base.Awake();

        healthBarImage = GetComponent<Image>();
        healthBarImage.color = ship.shipData.UIColour.value;

        ship.TakeDamageAction += OnTakeDamage;
        ship.LoseLifeAction += OnLoseLife;
    }

    void OnTakeDamage()
    {
        if (refillCoroutine != null)
            StopCoroutine(refillCoroutine);

        print(HealthPercent);
        refillCoroutine = UpdateFill(HealthPercent, animationDuration: 0.2f);
        StartCoroutine(refillCoroutine);
    }

    void OnLoseLife()
    {
        if (refillCoroutine != null)
            StopCoroutine(refillCoroutine);

        refillCoroutine = UpdateFill(1f, delay: 1f);
        StartCoroutine(refillCoroutine);
    }

    IEnumerator UpdateFill(float endFillAmount, float animationDuration = 0.5f, float delay = 0f)
    {
        yield return WaitForSeconds(delay);

        float startFillAmount = healthBarImage.fillAmount;
        float currentLerpTime = 0f;

        while (currentLerpTime < animationDuration)
        {
            float lerpProgress = currentLerpTime / animationDuration;
            healthBarImage.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, refillInterpolation.Evaluate(lerpProgress));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        refillCoroutine = null;
    }
}