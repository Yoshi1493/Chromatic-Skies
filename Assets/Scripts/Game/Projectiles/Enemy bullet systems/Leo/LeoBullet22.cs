using System.Collections;
using UnityEngine;

public class LeoBullet22 : ScriptableEnemyBullet<LeoBulletSystem21>
{
    const float EndSpeed = LeoBulletSystem21.BulletSpeed;
    const int BulletCount = 8;
    const float BulletSpacing = 8f;

    protected override float MaxLifetime => 2.5f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        float direction = Mathf.Sign(startSpeed);

        yield return this.LerpSpeed(startSpeed, 0f, 1f);

        yield return this.RotateBy(90f * direction, 0f);

        float endSpeed = Mathf.Sqrt((EndSpeed * EndSpeed) - (startSpeed * startSpeed)) * direction;
        yield return this.LerpSpeed(0f, endSpeed, 0.5f);
        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);

        moveDirection = transform.position - ownerShip.transform.position;
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