using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoMovementSystem4 : EnemyMovement
{
    LeoBulletSystem4 bulletSystem;

    protected override void Awake()
    {
        base.Awake();
        bulletSystem = parentShip.GetComponentInChildren<LeoBulletSystem4>();
    }

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);

        Vector3 p1 = new(0.8f * Random.Range(-screenHalfWidth, screenHalfWidth), 1.2f * screenHalfHeight);

        yield return this.MoveTo(p1, 1f);
        parentShip.Invincible = true;

        yield return WaitForSeconds(2f);
        parentShip.Invincible = false;

        List<Vector3> clonePositions = bulletSystem.bulletSpawnPositions;
        Vector3 p3 = clonePositions[Random.Range(0, clonePositions.Capacity)];
        Vector3 p2 = new(p3.x, p1.y);

        yield return this.MoveFromTo(p2, p3, 1f);
    }
}