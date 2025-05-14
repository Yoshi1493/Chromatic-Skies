using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem03 : CommonEnemyMovement
{
    [SerializeField] Vector2 rotationPoint;
    [SerializeField] float rotationAmount;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(rotationPoint, rotationAmount, 5f);

        yield return WaitForSeconds(1f);
        yield return LeaveScene();
    }
}