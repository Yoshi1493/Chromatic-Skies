using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    Enemy enemy;

    protected float screenHalfHeight;
    protected float screenHalfWidth;

    const int PrewarmCount = 500;

    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();

        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;
    }

    void Start()
    {
        enemy.LoseLifeAction += OnEnemyLoseLife;

        //pre-warm object pool
        for (int i = 0; i < PrewarmCount; i++)
        {
            SpawnCollectible(CollectibleType.Score, new Vector3(screenHalfWidth, screenHalfHeight));
        }
    }

    Collectible SpawnCollectible(CollectibleType collectibleType, Vector3 spawnPos)
    {
        var newCollectible = CollectibleObjectPool.Instance.Get((int)collectibleType);
        newCollectible.transform.position = spawnPos;

        return newCollectible;
    }

    void OnEnemyLoseLife()
    {
        foreach (var bullet in EnemyBulletPool.Instance.GetAllActiveObjects())
        {
            Vector3 pos = bullet.transform.position;

            if (-screenHalfWidth < pos.x && pos.x < screenHalfWidth && -screenHalfHeight < pos.y && pos.y < screenHalfHeight)
            {
                var collectible = SpawnCollectible(CollectibleType.Score, pos);
                collectible.gameObject.SetActive(true);
                collectible.enabled = true;
            }
        }
    }

    void OnDestroy()
    {
        enemy.LoseLifeAction -= OnEnemyLoseLife;
    }
}