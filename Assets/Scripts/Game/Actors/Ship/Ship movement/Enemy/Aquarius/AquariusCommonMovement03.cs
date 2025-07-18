using System.Collections;
using UnityEngine;

public class AquariusCommonMovement03 : CommonEnemyMovement
{
    [SerializeField] Vector2 rotationPoint;
    [SerializeField] float rotationAmount;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(rotationPoint, rotationAmount, 6f);

        yield return LeaveScene();
    }
}