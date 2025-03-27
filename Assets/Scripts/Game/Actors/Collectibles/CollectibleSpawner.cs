using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    Boss boss;

    protected float screenHalfHeight;
    protected float screenHalfWidth;

    const int PrewarmCount = 500;

    void Awake()
    {
        boss = FindObjectOfType<Boss>();

        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;
    }

    void Start()
    {
        boss.LoseLifeAction += OnBossLoseLife;
    }

    Collectible SpawnCollectible(CollectibleType collectibleType, Vector3 spawnPos)
    {
        var newCollectible = CollectibleObjectPool.Instance.Get((int)collectibleType);
        newCollectible.transform.position = spawnPos;

        return newCollectible;
    }

    void OnBossLoseLife()
    {
        foreach (var bullet in BossBulletPool.Instance.GetAllActiveObjects())
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
        boss.LoseLifeAction -= OnBossLoseLife;
    }
}