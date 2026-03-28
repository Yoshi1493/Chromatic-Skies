using UnityEngine;

public class BossLivesDisplay : LivesDisplay<Boss>
{
    [SerializeField] IntObject selectedBossIndex;
    EnemySpawner enemySpawner;

    protected override void Awake()
    {
        base.Awake();
        enemySpawner = FindAnyObjectByType<EnemySpawnerController>().GetComponentsInChildren<EnemySpawner>(true)[selectedBossIndex.value];
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(InitDisplay());
    }

    void Start()
    {
        enemySpawner.BossSpawnAction += OnBossSpawn;

        StopAllCoroutines();
        enabled = false;
    }
    void OnBossSpawn()
    {
        enabled = true;
    }

    void OnDisable()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }

    void OnDestroy()
    {
        enemySpawner.BossSpawnAction -= OnBossSpawn;
    }
}