using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Ship
{
    List<IEnemyAttack> activeEnemyShooters = new();

    void Start()
    {
        FindObjectOfType<Player>().LoseLifeAction += OnPlayerLoseLife;
    }

    protected override void Update()
    {
        base.Update();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(currentHealth);
#endif
    }

    protected override async void LoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - currentLives;

        base.LoseLife();
        EnemyBulletPool.Instance.DrainPool();

        if (currentLives > 0)
        {
            GetActiveEnemyShooters();
            IEnemyAttack nextEnemyShooter = transform.GetChild(currentProjectileSystem + 1).GetComponent<IEnemyAttack>();

            foreach (var enemyShooter in activeEnemyShooters)
            {
                enemyShooter.SetEnabled(false);
            }

            StartCoroutine(this.ReturnToOriginalPosition());
            await Task.Delay(RespawnTime);

            nextEnemyShooter.SetEnabled(true);
            SetSpriteAlpha(1f);
            collider.enabled = true;
        }
    }

    //disable and re-enable current projectile system upon player losing life
    async void OnPlayerLoseLife()
    {
        GetActiveEnemyShooters();

        foreach (var enemyShooter in activeEnemyShooters)
        {
            enemyShooter.SetEnabled(false);
        }

        StartCoroutine(this.MoveTo(transform.position, 1f));
        invincible = true;

        await Task.Delay(RespawnTime);

        activeEnemyShooters[0].SetEnabled(true);
        invincible = false;
    }

    void GetActiveEnemyShooters()
    {
        activeEnemyShooters.Clear();

        //check all children for active projectile systems
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out IEnemyAttack projectileSystem) && projectileSystem.Enabled)
            {
                activeEnemyShooters.Add(projectileSystem);
            }

            if (child.childCount > 0)
            {
                //check all children of children for active projectile subsystems
                for (int ii = 0; ii < child.childCount; ii++)
                {
                    if (child.GetChild(ii).TryGetComponent(out IEnemyAttack projectileSubsystem) && projectileSubsystem.Enabled)
                    {
                        activeEnemyShooters.Add(projectileSubsystem);
                    }
                }
            }
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}