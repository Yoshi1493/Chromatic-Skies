using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0, 0, 1, 1);
    static readonly Vector3 originalPosition = new Vector3(0f, 2.5f, 0f);

    /// <summary>
    /// translates <ship> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo(this Ship ship, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 startPosition = ship.transform.position;
        float currentLerpTime = 0f;

        while (ship.transform.position != endPosition)
        {
            ship.transform.position = Vector3.Lerp(startPosition, endPosition, moveInterpolation.Evaluate(currentLerpTime / moveDuration));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// translates <ship> to <GetRandomPosition()> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition(this Ship ship, float moveDuration, float delay = 0f)
    {
        Vector3 endPosition = GetRandomPosition(ship);
        yield return ship.MoveTo(endPosition, moveDuration, delay);        
    }

    public static IEnumerator ReturnToOriginalPosition(this Ship ship, float moveDuration = 1f, float delay = 0f)
    {
        yield return ship.MoveTo(originalPosition, moveDuration, delay);
    }

    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// returns a random Vector3 that is at least <minSqrMagDelta> units away from <ship.transform.position>.
    /// </summary>
    //to-do: fix; get rid of magic numbers
    static Vector3 GetRandomPosition(Ship ship, float minSqrMagDelta = 4f)
    {
        Vector3 newRandPos = new Vector3
            (
            Random.Range(-5f, 5f),
            Random.Range(2f, 4f)
            );

        while ((ship.transform.position - newRandPos).sqrMagnitude < minSqrMagDelta)
        {
            newRandPos = new Vector3
                (
                Random.Range(-5f, 5f),
                Random.Range(2f, 4f)
                );
        }

        return newRandPos;
    }

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}