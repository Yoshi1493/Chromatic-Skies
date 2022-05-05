using System.Collections;

public class LeoBullet22 : ScriptableEnemyBullet<LeoBulletSystem21>
{
    const int BulletCount = 8;
    const float BulletSpacing = 8f;

    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(5f, 0f, 1f));
        yield return this.RotateBy(30f, 1f);
    }

    public override void Destroy()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing + transform.eulerAngles.z;

            var bullet = SpawnBullet(3, z, transform.position, false);
            bullet.MoveSpeed = i * 0.25f + 1;
            bullet.Fire();
        }

        base.Destroy();
    }
}