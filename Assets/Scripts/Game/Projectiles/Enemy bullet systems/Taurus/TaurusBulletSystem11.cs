using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 150;
    public const float BulletSpacing = 1f;
    const float MinDistanceFromShip = BulletSpacing;

    List<Vector2> spawnPositions = new(BulletCount);
    List<EnemyBullet> bullets = new(BulletCount);

    protected override float ShootingCooldown => 0.02f;

    bool IsTooClose(Vector3 p) => (ownerShip.transform.position - p).sqrMagnitude < MinDistanceFromShip || (PlayerPosition - p).sqrMagnitude < MinDistanceFromShip;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        spawnPositions = GetRandomPointsWithinBounds(new Vector2(-camHalfWidth, -camHalfHeight), new Vector2(camHalfWidth, camHalfHeight), BulletCount);

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            float x = RoundToNearestMultipleOf(spawnPositions[i].x, BulletSpacing);
            float y = RoundToNearestMultipleOf(spawnPositions[i].y, BulletSpacing);
            Vector3 offset = BulletSpacing * 0.5f * Vector3.one;

            Vector3 spawnPos = new Vector3(x, y) - offset;
            spawnPositions[i] = spawnPos;
        }

        //cull spawn positions
        spawnPositions = spawnPositions.Where(p => !IsTooClose(p)).Distinct().ToList();

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            var bullet = SpawnProjectile(1, 45f, spawnPositions[i], false);
            bullets.Add(bullet);

            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.ForEach(b => b.Fire());
        bullets.Clear();

        enabled = false;
    }
}