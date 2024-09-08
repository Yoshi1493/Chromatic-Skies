using System.Collections;
using UnityEngine;

public class SagittariusBullet50 : ScriptableEnemyBullet<SagittariusBulletSystem5, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BranchCount = 2;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.1f;
    const float BulletRotationSpeedModifier = 2f;

    protected override float MaxLifetime => 1.5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, Random.Range(3f, 6f), 1f);
    }

    protected override void Update()
    {
        base.Update();

        float t = MaxLifetime - currentLifetime;

        if (t < 1f)
        {
            Color c = SpriteRenderer.color;
            c.a = t * 2f;
            SpriteRenderer.color = c;
        }
    }

    public override void Destroy()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = Random.Range(-BulletSpacing, BulletSpacing);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnBullet(1, z, pos, false);
                bullet.MoveSpeed = s;
            }
        }

        base.Destroy();
    }
}