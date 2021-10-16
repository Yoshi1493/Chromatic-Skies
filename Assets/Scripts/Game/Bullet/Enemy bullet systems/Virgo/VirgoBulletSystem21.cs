using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    Stack<EnemyBullet> bullets = new Stack<EnemyBullet>(84);

    readonly float a = 0.7f;
    readonly float b = 0.3f;

    protected override IEnumerator Shoot()
    {
        transform.Rotate(0f, 0f, Random.Range(-90f, 90f));

        for (int h = 0; h < 4; h++)
        {
            transform.Rotate(0f, 0f, 45f);

            for (int i = 0; i <= 10; i++)
            {
                //sigmoid curve (3(i/a)^2 - 2(i/a)^3) * b, where
                //i = iterator
                //a = the point between (0.0, 1.0] at which the curve evaluates to b
                //b = local maximum height
                float xPos = (3 * Mathf.Pow((1 - (i * 0.1f)) / a, 2) - 2 * Mathf.Pow((1 - (i * 0.1f)) / a, 3)) * b;
                float yPos = i / -10f;

                float zRot = (10 - i) * 10f;

                for (int j = 0; j < 360; j += 90)
                {
                    bulletData.colour = bulletData.gradient.Evaluate(-yPos);

                    bullets.Push(SpawnProjectile(5, zRot + j - transform.eulerAngles.z, 2 * transform.InverseTransformVector(xPos, yPos, 0f).RotateVectorBy(j)));
                    bullets.Push(SpawnProjectile(5, -zRot + j - transform.eulerAngles.z, 2 * transform.InverseTransformVector(-xPos, yPos, 0f).RotateVectorBy(j)));
                }

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            int bulletCount = bullets.Count;

            for (int i = 0; i < bulletCount; i++)
            {
                bullets.Pop().Fire();
            }
        }
        enabled = false;
    }
}