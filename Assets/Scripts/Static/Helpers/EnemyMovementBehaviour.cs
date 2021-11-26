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
    public static IEnumerator MoveToRandomPosition(this Ship ship, float moveDuration, float minSqrMagDelta = 2f, float maxSqrMagDelta = 4f, float delay = 0f)
    {
        if (minSqrMagDelta > maxSqrMagDelta)
            yield break;

        float randMagnitude = Random.Range(minSqrMagDelta, maxSqrMagDelta);
        Vector3 randDirection = Random.insideUnitCircle.normalized;

        while (Physics2D.Raycast(ship.transform.position, randDirection, randMagnitude, ship.shipData.boundaryLayer).collider != null)
        {
            randDirection = Random.insideUnitCircle.normalized;
        }

        Vector3 endPosition = ship.transform.position + (randMagnitude * randDirection);
        yield return ship.MoveTo(endPosition, moveDuration, delay);        
    }

    public static IEnumerator ReturnToOriginalPosition(this Ship ship, float moveDuration = 1f, float delay = 0f)
    {
        yield return ship.MoveTo(originalPosition, moveDuration, delay);
    }

    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}