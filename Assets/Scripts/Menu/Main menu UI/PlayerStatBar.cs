using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class PlayerStatBar : MonoBehaviour
{
    Image barImage;

    IEnumerator fillAnimation, colourAnimation;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    protected float maxStatValue;
    protected float[] fillAmounts;

    protected virtual void Awake()
    {
        barImage = GetComponent<Image>();

        SetFillAmounts();
    }

    public void AnimateBar(int playerIndex)
    {
        if (fillAnimation != null) StopCoroutine(fillAnimation);

        fillAnimation = LerpFillAmount(fillAmounts[playerIndex]);
        StartCoroutine(fillAnimation);

        if (colourAnimation != null) StopCoroutine(colourAnimation);

        colourAnimation = LerpColour(Color.gray);
        StartCoroutine(colourAnimation);
    }

    IEnumerator LerpFillAmount(float endFill)
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

    IEnumerator LerpColour(Color endColour)
    {
        Color startColour = barImage.color;
        float currentLerpTime = 0f, totalLerpTime = 0.1f;

        while (barImage.color != endColour)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / totalLerpTime);
            barImage.color = Color.Lerp(startColour, endColour, animationProgress);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    public virtual void SetFillAmounts()
    {
        AnimateBar(0);
    }
}