using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

[RequireComponent(typeof(Image))]
public class PlayerStatBar : MonoBehaviour
{
    Image statBarImage;

    IEnumerator statBarAnimation;
    const float AnimationDuration = 0.25f;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Awake()
    {
        statBarImage = GetComponent<Image>();
    }

    //called upon initialization in StatBarController and end of AnimateStatBar coroutine
    public void SetStatBar(float amount, Color colour)
    {
        if (statBarAnimation != null)
        {
            StopCoroutine(statBarAnimation);
        }

        statBarImage.fillAmount = amount;
        statBarImage.color = colour;
    }

    public void AnimateStatBar(float endFillAmount, Color endColour)
    {
        if (statBarAnimation != null)
        {
            StopCoroutine(statBarAnimation);
        }

        statBarAnimation = _AnimateStatBar(endFillAmount, endColour);
        StartCoroutine(statBarAnimation);
    }

    IEnumerator _AnimateStatBar(float endFillAmount, Color endColour)
    {
        float startFillAmount = statBarImage.fillAmount;
        Color startColour = statBarImage.color;

        float currentLerpTime = 0f;

        while (currentLerpTime <= AnimationDuration)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / AnimationDuration);

            statBarImage.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, animationProgress);
            statBarImage.color = Color.Lerp(startColour, endColour, animationProgress);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        SetStatBar(endFillAmount, endColour);

        statBarAnimation = null;
    }
}