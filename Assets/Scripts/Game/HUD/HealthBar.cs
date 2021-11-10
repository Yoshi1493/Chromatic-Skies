using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

public class HealthBar<TShip> : HUDComponent<TShip>
    where TShip : Ship
{
    Image healthBarImage;
    float HealthPercent => (float)ship.shipData.CurrentHealth.Value / ship.shipData.MaxHealth.Value;

    [SerializeField] AnimationCurve fillInterpolation;
    IEnumerator fillCoroutine;

    protected override void Awake()
    {
        base.Awake();

        healthBarImage = GetComponent<Image>();
        healthBarImage.color = ship.shipData.UIColour.value;

        ship.TakeDamageAction += OnTakeDamage;
        ship.LoseLifeAction += OnLoseLife;
    }

    protected virtual void Start()
    {
        fillCoroutine = Refill();
        StartCoroutine(fillCoroutine);
    }

    void OnTakeDamage()
    {
        if (fillCoroutine == null)
            healthBarImage.fillAmount = HealthPercent;
    }

    void OnLoseLife()
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = Refill();
        StartCoroutine(fillCoroutine);
    }

    IEnumerator Refill()
    {
        healthBarImage.fillAmount = 0f;

        if (ship.shipData.CurrentLives.Value <= 0) yield break;

        yield return WaitForSeconds(1f);

        float currentLerpTime = 0f;
        float animDuration = 0.5f;

        while (currentLerpTime < animDuration)
        {
            float lerpProgress = currentLerpTime / animDuration;
            healthBarImage.fillAmount = Mathf.Lerp(0f, HealthPercent, fillInterpolation.Evaluate(lerpProgress));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        fillCoroutine = null;
    }
}