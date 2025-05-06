using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class LeoMovementSystem4 : BossMovement
{
    LeoBossShooter4 bulletSystem;
    List<Vector3> clonePositions;

    protected override void Awake()
    {
        base.Awake();
        bulletSystem = parentShip.GetComponentInChildren<LeoBossShooter4>();
    }

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);

        Vector3 p1 = 1.2f * new Vector3(-ScreenHalfWidth, ScreenHalfHeight);
        yield return parentShip.MoveTo(p1, 1f);
        parentShip.Invincible = true;

        yield return WaitForSeconds(1f);
        parentShip.Invincible = false;

        clonePositions = bulletSystem.bulletSpawnPositions;

        Vector3 p2 = new(1.1f * -ScreenHalfWidth, clonePositions[0].y + Random.Range(-0.5f, 0.5f));
        parentShip.transform.position = p2;

        for (int i = 0; i < clonePositions.Count; i++)
        {
            yield return parentShip.MoveTo(clonePositions[i], 1f);
        }

        Vector3 p3 = new(1.1f * ScreenHalfWidth, clonePositions[^1].y + Random.Range(-0.5f, 0.5f));

        yield return parentShip.MoveTo(p3, 0.5f);

        Vector3 p5 = clonePositions[Random.Range(0, clonePositions.Count)];
        Vector3 p4 = new(p5.x, 1.1f * ScreenHalfHeight);

        yield return parentShip.MoveFromTo(p4, p5, 1f);
    }
}