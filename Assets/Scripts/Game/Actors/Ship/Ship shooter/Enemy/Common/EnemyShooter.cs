using System.Collections;
using UnityEngine;

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>
    where TProjectile : Projectile
{

}