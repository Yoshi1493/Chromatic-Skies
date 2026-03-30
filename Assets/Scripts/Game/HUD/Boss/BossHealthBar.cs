using UnityEngine;

public class BossHealthBar : HealthBar<Boss>
{
    [SerializeField] IntObject selectedBossIndex;
    EnemySpawner enemySpawner;

    protected override void Awake()
    {
        base.Awake();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Start()
    {
        enemySpawner.BossSpawnAction += OnBossSpawn;
        enabled = false;
    }
    void OnBossSpawn()
    {
        enabled = true;
    }

    void OnDisable()
    {
        healthBarImage.enabled = false;
    }

    void OnDestroy()
    {
        enemySpawner.BossSpawnAction -= OnBossSpawn;
    }
}