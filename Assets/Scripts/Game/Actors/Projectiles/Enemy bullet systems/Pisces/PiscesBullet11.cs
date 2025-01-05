using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet11 : ScriptableEnemyBullet<PiscesBulletSystem11, EnemyBullet>
{
    [Space]
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 10;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
        yield return WaitForSeconds(1f);

        Vector3 pos = transform.position;
        float r = playerShip.transform.position.GetRotationDifference(pos);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));
                SpawnBullet(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        float currentLerpTime = 0f;
        float totalLerpTime = 0.5f;

        while (currentLerpTime < totalLerpTime)
        {
            Color c = SpriteRenderer.color;
            c.a = Mathf.Lerp(1f, 0f, currentLerpTime / totalLerpTime);
            SpriteRenderer.color = c;

            yield return null;
            currentLerpTime += Time.deltaTime;
        }

        Destroy();
    }
}