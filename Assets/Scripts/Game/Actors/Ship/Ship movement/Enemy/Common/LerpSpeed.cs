using System.Collections;
using UnityEngine;

public class LerpSpeed : CommonEnemyMovement
{
    [SerializeField] float startSpeed;
    [SerializeField] float endSpeed;

    protected override IEnumerator Move()
    {
        yield return parentShip.LerpSpeed(startSpeed, endSpeed, duration, delay);
    }
}