using System.Collections;
using UnityEngine;

public class TranslateAround : EnemyMovement
{
    [SerializeField] float delay;
    [SerializeField] float rotationAmount;
    [SerializeField] float rotationDuration;
    [SerializeField] Vector2 rotationPoint;

    protected override IEnumerator Move()
    {
        yield return this.TranslateAround(rotationPoint, rotationAmount, rotationDuration, delay);
    }
}