using System.Collections;
using UnityEngine;
using static CoroutineHelper;

//helper class to handle ship movement
public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public static readonly Vector3 bossSpawnPosition = new(0f, 2.5f, 0f);
    public static readonly Vector3 playerSpawnPosition = new(0f, -2f, 0f);

    /// <summary>
    /// translates <ship> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo<TShip>(this ShipMovement<TShip> ship, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);
        if (moveDuration <= 0f) ship.parentShip.transform.position = endPosition;

        Vector3 startPosition = ship.transform.position;
        ship.moveDirection = endPosition - startPosition;
        float maxSpeed = ship.moveDirection.magnitude;

        float currentTime = 0f;
        while (currentTime < moveDuration / 2f)
        {
            ship.currentSpeed = Mathf.Lerp(0f, maxSpeed, moveInterpolation.Evaluate(currentTime * 2f / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        currentTime = 0f;
        while (currentTime < moveDuration / 2f)
        {
            ship.currentSpeed = Mathf.Lerp(maxSpeed, 0f, moveInterpolation.Evaluate(currentTime * 2f / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        ship.parentShip.transform.position = endPosition;
        ship.currentSpeed = 0f;
    }

    public static IEnumerator MoveFromTo<TShip>(this ShipMovement<TShip> ship, Vector3 startPosition, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        ship.parentShip.transform.position = startPosition;
        yield return ship.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// linearly translates <ship> to <endPosition> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToLinear<TShip>(this ShipMovement<TShip> ship, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - ship.transform.position;
        float newMoveSpeed = newMoveDirection.magnitude / moveDuration;

        ship.moveDirection = newMoveDirection;
        ship.currentSpeed = newMoveSpeed;

        yield return WaitForSeconds(moveDuration);

        ship.parentShip.transform.position = endPosition;
        ship.currentSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> to <GetRandomPositionWithinBounds()> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition<TShip>(this ShipMovement<TShip> ship, float moveDuration, float minDeltaMagnitude = 2f, float maxDeltaMagnitude = 4f, float delay = 0f)
        where TShip : Ship
    {
        if (minDeltaMagnitude > maxDeltaMagnitude) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 endPosition = ship.transform.position.GetRandomPositionWithinBounds(ship.shipData.boundaryLayer, minDeltaMagnitude, maxDeltaMagnitude);
        yield return ship.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// translates <ship> clockwise around <point> by <degrees> degrees over <duration> seconds.
    /// </summary>
    public static IEnumerator TranslateAround<TShip>(this ShipMovement<TShip> ship, Vector3 point, float degrees, float duration, float delay = 0f)
        where TShip : Ship
    {
        if (degrees == 0f || duration <= 0f) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 diff = point - ship.transform.position;
        ship.currentSpeed = diff.magnitude * degrees * Mathf.Deg2Rad / duration;

        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
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

    public static IEnumerator ReturnToOriginalPosition<TShip>(this ShipMovement<TShip> ship, float moveDuration = 1f, float delay = 0f)
        where TShip : Ship
    {
        if (ship is Boss)
        {
            yield return ship.MoveTo(bossSpawnPosition, moveDuration, delay);
        }

        if (ship is Player)
        {
            yield return ship.MoveTo(playerSpawnPosition, moveDuration, delay);
        }
    }
    #endregion

    #region Debug

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}