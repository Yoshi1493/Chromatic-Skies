using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

[RequireComponent(typeof(Image))]
public class HealthBar<TShip> : ShipHUDComponent<TShip>
    where TShip : Ship
{
    Image healthBarImage;
    float HealthPercent => (float)ship.currentHealth / ship.shipData.MaxHealth.Value;

    IEnumerator fillCoroutine;
    const float FillAnimationDuration = 0.5f;
    [SerializeField] AnimationCurve fillInterpolation;

    protected override void Awake()
    {
        base.Awake();

        healthBarImage = GetComponent<Image>();

        if (ship != null)
        {
            ship.TakeDamageAction += OnTakeDamage;
            ship.RespawnAction += OnRespawn;

            healthBarImage.color = ship.shipData.UIColour.value;
        }
    }

    void Start()
    {
        fillCoroutine = Refill();
        StartCoroutine(fillCoroutine);
    }

    void OnTakeDamage()
    {
        if (fillCoroutine == null)
        {
            healthBarImage.fillAmount = HealthPercent;
        }
    }

    void OnRespawn()
    {
        if (fillCoroutine == null)
        {
            fillCoroutine = Refill();
            StartCoroutine(fillCoroutine);
        }
    }

    IEnumerator Refill()
    {
        healthBarImage.fillAmount = 0f;

        if (ship.currentLives <= 0) yield break;

        float currentLerpTime = 0f;

        while (currentLerpTime < FillAnimationDuration)
        {
            float lerpProgress = currentLerpTime / FillAnimationDuration;
            healthBarImage.fillAmount = Mathf.Lerp(0f, HealthPercent, fillInterpolation.Evaluate(lerpProgress));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        healthBarImage.fillAmount = HealthPercent;

        fillCoroutine = null;
    }
}