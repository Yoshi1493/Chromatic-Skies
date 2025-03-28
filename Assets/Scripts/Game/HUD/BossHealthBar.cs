using UnityEngine;

public class BossHealthBar : HealthBar<Boss>
{
    [SerializeField] EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner.BossSpawnAction += () => enabled = true;
        enabled = false;
    }

    void OnDisable()
    {
        healthBarImage.enabled = false;
    }
}