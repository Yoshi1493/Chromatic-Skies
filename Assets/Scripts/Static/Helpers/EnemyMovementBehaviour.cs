using System.Collections;
using UnityEngine;
using static CoroutineHelper;

//"helper" class to handle enemy movement
public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    static readonly Vector3 originalPosition = new Vector3(0f, 2.5f, 0f);

    /// <summary>
    /// translates <ship> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo(this Ship ship, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - ship.transform.position;
        float newMoveSpeed = 2 * newMoveDirection.magnitude / moveDuration;

        float currentTime = 0f;
        ship.moveDirection = newMoveDirection;

        while (currentTime < moveDuration / 2f)
        {
            ship.currentSpeed = Mathf.Lerp(0, newMoveSpeed, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        currentTime = 0f;

        while (currentTime < moveDuration / 2f)
        {
            ship.currentSpeed = Mathf.Lerp(newMoveSpeed, 0, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        ship.transform.position = endPosition;
        ship.currentSpeed = 0f;
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
            randMagnitude = Random.Range(minSqrMagDelta, maxSqrMagDelta);
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