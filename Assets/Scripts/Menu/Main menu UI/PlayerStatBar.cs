using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class PlayerStatBar : MonoBehaviour
{
    Image barImage;

    IEnumerator fillAnimation;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    protected float maxStatValue;
    protected float[] fillAmounts;

    protected virtual void Awake()
    {
        barImage = GetComponent<Image>();

        SetFillAmounts();
    }

    public void LerpFillAmount(int index)
    {
        if (fillAnimation != null) StopCoroutine(fillAnimation);

        fillAnimation = _LerpFillAmount(fillAmounts[index]);
        StartCoroutine(fillAnimation);
    }

    IEnumerator _LerpFillAmount(float endFill)
    {
        float startFill = barImage.fillAmount;
        float currentLerpTime = 0f, totalLerpTime = 0.2f;

        while (barImage.fillAmount != endFill)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / totalLerpTime);
            barImage.fillAmount = Mathf.Lerp(startFill, endFill, animationProgress);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    public virtual void SetFillAmounts()
    {
        LerpFillAmount(0);
    }
}