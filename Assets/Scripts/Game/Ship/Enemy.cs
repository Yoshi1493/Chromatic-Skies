using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Ship
{
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

        if (currentLives > 0)
        {
            var currentEnemyShooter = transform.GetChild(currentProjectileSystem).GetComponentInChildren<IEnemyAttack>();
            var nextEnemyShooter = transform.GetChild(currentProjectileSystem + 1).GetComponent<IEnemyAttack>();

            currentEnemyShooter.SetEnabled(false);

            StartCoroutine(this.ReturnToOriginalPosition());
            await Task.Delay(RespawnTime * 2);

            nextEnemyShooter.SetEnabled(true);
            SetSpriteAlpha(1f);
            collider.enabled = true;
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    //disable and re-enable current projectile system upon player losing life
    async void OnPlayerLoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - currentLives;
        var currentEnemyShooters = GetActiveEnemyShooters();

        foreach (var enemyShooter in currentEnemyShooters)
        {
            enemyShooter.SetEnabled(false);
            print($"disabled {enemyShooter}.");
        }

        await Task.Delay(RespawnTime);

        //if player didn't kill enemy between delay
        if (currentProjectileSystem == shipData.MaxLives.Value - currentLives)
        {
            currentEnemyShooters[0].SetEnabled(true);
            print($"re-enabled {currentEnemyShooters[0]}.");
        }
    }

    List<IEnemyAttack> GetActiveEnemyShooters()
    {
        List<IEnemyAttack> activeEnemyShooters = new List<IEnemyAttack>();

        //check all children for active projectile systems
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out IEnemyAttack projectileSystem))
            {
                if (projectileSystem.Enabled)
                {
                    activeEnemyShooters.Add(projectileSystem);
                    print($"found {projectileSystem}");
                }
            }

            if (child.childCount > 0)
            {
                //check all children of children for active projectile subsystems
                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).TryGetComponent(out IEnemyAttack projectileSubsystem))
                    {
                        activeEnemyShooters.Add(projectileSubsystem);
                        print($"found {projectileSubsystem}");
                    }
                }
            }

        }

        return activeEnemyShooters;
    }
}