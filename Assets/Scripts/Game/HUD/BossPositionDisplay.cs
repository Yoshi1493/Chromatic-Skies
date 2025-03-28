using UnityEngine;

public class BossPositionDisplay : ShipHUDComponent<Boss>
{
    new Transform transform;
    SpriteRenderer spriteRenderer;

    [SerializeField] EnemySpawner enemySpawner;

    protected override void Awake()
    {
        base.Awake();

        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (ship != null)
        {
            ship.DeathAction += () => SetActive(false);
        }
        else
        {
            enabled = false;
            SetActive(false);
        }
    }

    void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    void Start()
    {
        enemySpawner.BossSpawnAction += () => enabled = true;
        enabled = false;
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
}