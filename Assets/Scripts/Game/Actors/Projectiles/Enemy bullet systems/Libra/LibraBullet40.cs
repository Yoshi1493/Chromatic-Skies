using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet40 : ScriptableEnemyBullet<LibraBulletSystem41, Laser>
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.RotateBy(180f, 0f);
        yield return this.LerpSpeed(0f, 10f, 2f);
        yield return WaitUntil(() => transform.position.y < (-5f - SpriteRenderer.size.y));
        MoveSpeed = 0f;

        float z = transform.eulerAngles.z + 180f;
        Vector3 pos = transform.position;

        SpawnBullet(0, z, pos, false).Fire(0.1f);
    }
}