using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    readonly Vector2 MinBounds = new(-7f, 0f);
    readonly Vector2 MaxBounds = new(7f, 4f);
    const int BulletRowCount = 6;
    const int BulletColCount = 21;
    const int BulletIDCount = 5;

    List<List<Vector2>> bulletSpawnPositions = new();

    const int AttackPatternCount = 4;
    IEnumerator currentAttackPattern;

    protected override void Awake()
    {
        base.Awake();

        bulletSpawnPositions.Clear();

        for (int i = 0; i <= BulletColCount; i++)
        {
            bulletSpawnPositions.Add(new());
            float x = Mathf.Lerp(MinBounds.x, MaxBounds.x, i / (float)BulletColCount);

            for (int ii = 0; ii <= BulletRowCount; ii++)
            {
                float y = Mathf.Lerp(MinBounds.y, MaxBounds.y, ii / (float)BulletRowCount);
                bulletSpawnPositions[i].Add(new(x, y));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; i < bulletSpawnPositions.Count; i++)
        {
            for (int ii = 0; ii < bulletSpawnPositions[i].Count; ii++)
            {
                float z = 0f;
                Vector3 pos = bulletSpawnPositions[i][ii];

                SpawnProjectile(0, z, pos, false);
            }

            yield return WaitForSeconds(ShootingCooldown * 0.5f);
        }

        yield return WaitForSeconds(0.5f);

        int r, r0 = -1;

        while (enabled)
        {
            if (currentAttackPattern != null)
            {
                StopCoroutine(currentAttackPattern);
                currentAttackPattern = null;
            }

            do
            {
                r = Random.Range(0, AttackPatternCount);
            }
            while (r == r0);
            r0 = r;

            switch (r)
            {
                case 0:
                    currentAttackPattern = FireSquare();
                    break;

                case 1:
                    currentAttackPattern = FireLine();
                    break;

                case 2:
                    currentAttackPattern = FireCross();
                    break;

                case 3:
                    currentAttackPattern = FireSingles();
                    break;

                default:
                    break; 
            }

            yield return currentAttackPattern;
            yield return WaitForSeconds(2f);
        }
    }

    IEnumerator FireSquare()
    {
        int squareSideLength = Random.Range(4, BulletRowCount + 1);
        int col = Random.Range(0, BulletColCount - (squareSideLength - 1));
        int row = Random.Range(0, BulletRowCount - (squareSideLength - 1));
        int b = Random.Range(1, BulletIDCount);
        float z = 0f;

        int numPasses = squareSideLength * 2 - 1;

        for (int i = 0; i < numPasses; i++)
        {
            int diagonalLength = (int)Mathf.PingPong(i, squareSideLength - 1) + 1;

            for (int ii = 0; ii < diagonalLength;)
            {
                for (int c = 0; c < squareSideLength; c++)
                {
                    for (int r = 0; r < squareSideLength; r++)
                    {
                        if (c + r == i)
                        {
                            Vector3 pos = bulletSpawnPositions[col + c][row + r];
                            SpawnProjectile(b, z, pos, false).Fire();

                            ii++;
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }

    IEnumerator FireLine()
    {
        int col = Random.Range(0, BulletColCount);
        int row = Random.Range(0, BulletRowCount);
        int b = Random.Range(1, BulletIDCount);
        float z = 0f;

        int maxLineLength = Mathf.Max(col, BulletColCount - col);

        Vector3 pos = bulletSpawnPositions[col][row];
        SpawnProjectile(b, z, pos, false).Fire();

        yield return WaitForSeconds(ShootingCooldown);

        for (int i = 1; i <= maxLineLength; i++)
        {
            if (col + i <= BulletColCount)
            {
                pos = bulletSpawnPositions[col + i][row];
                SpawnProjectile(b, z, pos, false).Fire();
            }

            if (col - i >= 0)
            {
                pos = bulletSpawnPositions[col - i][row];
                SpawnProjectile(b, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    IEnumerator FireCross()
    {
        int crossLength = BulletRowCount / 2;
        int col = Random.Range(crossLength, BulletColCount - (crossLength - 1));
        int row = BulletRowCount / 2;
        int b = Random.Range(1, BulletIDCount);
        float z = 0f;

        Vector3 pos = bulletSpawnPositions[col][row];
        SpawnProjectile(b, z, pos, false).Fire();

        for (int i = 1; i <= crossLength; i++)
        {
            pos = bulletSpawnPositions[col + i][row + i];
            SpawnProjectile(b, z, pos, false).Fire();

            pos = bulletSpawnPositions[col + i][row - i];
            SpawnProjectile(b, z, pos, false).Fire();

            pos = bulletSpawnPositions[col - i][row - i];
            SpawnProjectile(b, z, pos, false).Fire();

            pos = bulletSpawnPositions[col - i][row + i];
            SpawnProjectile(b, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    IEnumerator FireSingles()
    {
        int bulletCount = Random.Range(6, 11) * 2;
        List<(int c, int r)> spawnPositions = new(bulletCount);
        int b = Random.Range(1, BulletIDCount);
        float z = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            int col;
            int row;

            do
            {
                col = Random.Range(0, BulletColCount);
                row = Random.Range(0, BulletRowCount);
            }
            while (spawnPositions.Contains((col, row)));

            spawnPositions.Add((col, row));
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            (int c, int r) = (spawnPositions[i].c, spawnPositions[i].r);
            Vector3 pos = bulletSpawnPositions[c][r];

            SpawnProjectile(b, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}