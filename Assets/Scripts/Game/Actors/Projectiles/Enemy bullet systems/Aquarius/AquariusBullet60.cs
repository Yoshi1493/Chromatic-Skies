using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBullet60 : ScriptableEnemyBullet<AquariusBulletSystem6, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        float r = Mathf.PingPong(transform.eulerAngles.z, 180f) * -Mathf.Sign(transform.eulerAngles.z - 180f);

        StartCoroutine(this.RotateBy(r, 1.5f));
        yield return this.LerpSpeed(4f, 1f, 1f);
        yield return this.LerpSpeed(1f, 10f, 3f);

        yield return WaitUntil(() => transform.position.y < (-5f - SpriteRenderer.size.y));
        MoveSpeed = 0f;
    }

    public override void Destroy()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing);
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}