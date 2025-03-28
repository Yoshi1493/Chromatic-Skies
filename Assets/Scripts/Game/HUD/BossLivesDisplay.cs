using UnityEngine;

public class BossLivesDisplay : LivesDisplay<Boss>
{
    [SerializeField] EnemySpawner enemySpawner;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(InitDisplay());
    }

    void Start()
    {
        enemySpawner.BossSpawnAction += () => enabled = true;

        StopAllCoroutines();
        enabled = false;
    }

    void OnDisable()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }
}