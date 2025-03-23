using System.Collections.Generic;

public class ProjectileObjectPool<TProjectile> : GenericObjectPool<TProjectile> where TProjectile : Projectile
{
    public override void UpdatePoolableObjects(List<TProjectile> projectiles)
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            // check if projectile type already exists in object pool
            // this happens if the players loses a life when not all of the projectiles are pooled at the beginning of an attack pattern
            if (!objectPool.Exists(p => p.projectile.ProjectileID == projectiles[i].ProjectileID))
            {
                objectPool.Add((projectiles[i], new Queue<TProjectile>()));
            }
        }
    }

    public override TProjectile Get(int ID)
    {
        if (objectPool[ID].queue.Count > 0)
        {
            return objectPool[ID].queue.Dequeue();
        }
        else
        {
            TProjectile newProjectile = Instantiate(objectPool[ID].projectile, transform);
            newProjectile.enabled = false;

            return newProjectile;
        }
    }

    public override void ReturnToPool(TProjectile returningObject)
    {
        returningObject.transform.parent = transform;
        returningObject.gameObject.SetActive(false);
        returningObject.enabled = false;

        objectPool[returningObject.ProjectileID].queue.Enqueue(returningObject);
    }
}