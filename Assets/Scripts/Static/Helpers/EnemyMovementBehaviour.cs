using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class EnemyMovementBehaviour
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public static IEnumerator MoveTo(this Ship ship, Vector3 endPosition, float moveDuration, float delay = 0f)
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector2 startPosition = ship.transform.position;
        float currentLerpTime = 0f;

        while (ship.transform.position != endPosition)
        {
            ship.transform.position = Vector2.Lerp(startPosition, endPosition, moveInterpolation.Evaluate(currentLerpTime / moveDuration));

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    public static IEnumerator MoveToRandomPosition(this Ship ship, float moveDuration, float delay = 0f)
    {
        Vector2 endPosition = GetRandomPosition();
        yield return ship.MoveTo(endPosition, moveDuration, delay);        
    }

    #endregion

    #region Helpers/Extensions

    //to-do: fix; get rid of magic numbers
    static Vector2 GetRandomPosition()
    {
        return new Vector2 (
            Random.Range(-5f, 5f),
            Random.Range(2f, 4f)
            );
    }

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}