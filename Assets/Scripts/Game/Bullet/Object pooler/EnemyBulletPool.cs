using UnityEngine;

public class EnemyBulletPool : GenericBulletPool<EnemyBullet>
{
    [SerializeField] EnemyBulletSystem[] bulletSystems;


    public void UpdateObjectsToPool()
    {

    }
}