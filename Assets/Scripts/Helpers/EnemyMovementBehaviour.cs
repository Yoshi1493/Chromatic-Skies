using System.Collections;
using UnityEngine;
using static CoroutineHelper;

//"helper" class to handle enemy movement
public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    static readonly Vector3 originalPosition = new(0f, 2.5f, 0f);

    /// <summary>
    /// translates <enemy> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo(this EnemyMovement enemy, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - enemy.transform.position;
        float newMoveSpeed = 2f * newMoveDirection.magnitude / moveDuration;

        float currentTime = 0f;
        enemy.moveDirection = newMoveDirection;

        while (currentTime < moveDuration * 0.5f)
        {
            enemy.currentSpeed = Mathf.Lerp(0, newMoveSpeed, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        currentTime = 0f;

        while (currentTime < moveDuration * 0.5f)
        {
            enemy.currentSpeed = Mathf.Lerp(newMoveSpeed, 0, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        enemy.parentShip.transform.position = endPosition;
        enemy.currentSpeed = 0f;
    }

    public static IEnumerator MoveFromTo(this EnemyMovement enemy, Vector3 startPosition, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - startPosition;
        float newMoveSpeed = 2f * newMoveDirection.magnitude / moveDuration;

        float currentTime = 0f;

        enemy.parentShip.transform.position = startPosition;
        enemy.moveDirection = newMoveDirection;

        while (currentTime < moveDuration * 0.5f)
        {
            enemy.currentSpeed = Mathf.Lerp(0, newMoveSpeed, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        currentTime = 0f;

        while (currentTime < moveDuration * 0.5f)
        {
            enemy.currentSpeed = Mathf.Lerp(newMoveSpeed, 0, moveInterpolation.Evaluate(2f * currentTime / moveDuration));

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        enemy.parentShip.transform.position = endPosition;
        enemy.currentSpeed = 0f;
    }

    /// <summary>
    /// linearly translates <enemy> to <endPosition> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToLinear(this EnemyMovement enemy, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - enemy.transform.position;
        float newMoveSpeed = newMoveDirection.magnitude / moveDuration;

        enemy.moveDirection = newMoveDirection;
        enemy.currentSpeed = newMoveSpeed;

        yield return WaitForSeconds(moveDuration);

        enemy.parentShip.transform.position = endPosition;
        enemy.currentSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> to <GetRandomPosition()> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition(this EnemyMovement enemy, float moveDuration, float minSqrMagDelta = 2f, float maxSqrMagDelta = 4f, float delay = 0f)
    {
        if (minSqrMagDelta > maxSqrMagDelta) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        float randMagnitude;
        Vector3 randDirection;

        do
        {
            randMagnitude = Random.Range(minSqrMagDelta, maxSqrMagDelta);
            randDirection = Random.insideUnitCircle.normalized;
        }
        while (Physics2D.Raycast(enemy.transform.position, randDirection, randMagnitude, enemy.shipData.boundaryLayer).collider != null);

        Vector3 endPosition = enemy.transform.position + (randMagnitude * randDirection);
        yield return enemy.MoveTo(endPosition, moveDuration);
    }

    public static IEnumerator ReturnToOriginalPosition(this EnemyMovement enemy, float moveDuration = 1f, float delay = 0f)
    {
        yield return enemy.MoveTo(originalPosition, moveDuration, delay);
    }

    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}