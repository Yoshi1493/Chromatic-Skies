using UnityEngine;

public class BossPositionDisplay : ShipHUDComponent<Boss>
{
    new Transform transform;
    SpriteRenderer spriteRenderer;

    [SerializeField] IntObject selectedBossIndex;
    EnemySpawner enemySpawner;

    protected override void Awake()
    {
        base.Awake();

        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemySpawner = FindObjectOfType<EnemySpawnerController>().GetComponentsInChildren<EnemySpawner>(true)[selectedBossIndex.value];
    }

    void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    void Start()
    {
        enemySpawner.BossSpawnAction += OnBossSpawn;

        ship.DeathAction += OnBossDespawn;
        OnBossDespawn();
    }

    void OnBossSpawn()
    {
        SetActive(true);
    }

    void OnBossDespawn()
    {
        SetActive(false);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = ship.transform.position.x;
        transform.position = pos;
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    void OnDestroy()
    {
        enemySpawner.BossSpawnAction -= OnBossSpawn;
    }
}