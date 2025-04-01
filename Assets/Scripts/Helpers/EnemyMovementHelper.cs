using System.Collections;
using UnityEngine;
using static CoroutineHelper;

//"helper" class to handle boss movement
public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public static readonly Vector3 originalPosition = new(0f, 2.5f, 0f);

    /// <summary>
    /// translates <boss> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo(this BossMovement boss, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 startPosition = boss.transform.position;
        boss.moveDirection = endPosition - startPosition;
        float maxSpeed = boss.moveDirection.magnitude;

        float currentTime = 0f;
        while (currentTime < moveDuration / 2f)
        {
            boss.currentSpeed = Mathf.Lerp(0f, maxSpeed, moveInterpolation.Evaluate(currentTime * 2f / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        currentTime = 0f;
        while (currentTime < moveDuration / 2f)
        {
            boss.currentSpeed = Mathf.Lerp(maxSpeed, 0f, moveInterpolation.Evaluate(currentTime * 2f / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        boss.parentShip.transform.position = endPosition;
    }

    public static IEnumerator MoveFromTo(this BossMovement boss, Vector3 startPosition, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        boss.parentShip.transform.position = startPosition;
        yield return boss.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// linearly translates <boss> to <endPosition> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToLinear(this BossMovement boss, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - boss.transform.position;
        float newMoveSpeed = newMoveDirection.magnitude / moveDuration;

        boss.moveDirection = newMoveDirection;
        boss.currentSpeed = newMoveSpeed;

        yield return WaitForSeconds(moveDuration);

        boss.parentShip.transform.position = endPosition;
        boss.currentSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> to <GetRandomPosition()> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition(this BossMovement boss, float moveDuration, float minDeltaMagnitude = 2f, float maxDeltaMagnitude = 4f, float delay = 0f)
    {
        if (minDeltaMagnitude > maxDeltaMagnitude) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 endPosition = boss.transform.position.GetRandomPositionWithinBounds(boss.shipData.boundaryLayer, minDeltaMagnitude, maxDeltaMagnitude);
        yield return boss.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// returns a random position that is <minSqrMagDelta> to <maxSqrMagDelta> units away from <currentPostion>, within <bounds>
    /// </summary>
    public static Vector3 GetRandomPositionWithinBounds(this Vector3 currentPosition, LayerMask bounds, float minDeltaMagnitude, float maxDeltaMagnitude)
    {
        float randMagnitude;
        Vector3 randDirection;

        do
        {
            randMagnitude = Random.Range(minDeltaMagnitude, maxDeltaMagnitude);
            randDirection = Random.insideUnitCircle.normalized;
        }
        while (Physics2D.Raycast(currentPosition, randDirection, randMagnitude, bounds).collider != null);

        return currentPosition + (randMagnitude * randDirection);
    }

    public static IEnumerator ReturnToOriginalPosition(this BossMovement boss, float moveDuration = 1f, float delay = 0f)
    {
        yield return boss.MoveTo(originalPosition, moveDuration, delay);
    }
    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}