using UnityEngine;

public class BossHealthDisplay : HealthDisplay<Boss>
{
    [SerializeField] EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner.BossSpawnAction += () => enabled = true;
        enabled = false;
    }

    void OnDisable()
    {
        healthText.enabled = false;
    }
}