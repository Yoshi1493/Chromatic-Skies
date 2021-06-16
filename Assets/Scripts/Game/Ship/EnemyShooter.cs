using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter
{
    [SerializeField] BulletSystem[] attackPhases;

    IEnumerator Start()
    {
        yield return CoroutineHelper.WaitForSeconds(3);
        SpawnBullet(0);
    }

    protected override void SpawnBullet(int index)
    {
        attackPhases[index].Play();
    }
}