using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    Ship parentShip;

    [SerializeField] ShipObject[] playerData;
    [SerializeField] IntObject selectedPlayerIndex;
    [SerializeField] IntObject playerCurrentHealth;

    void Awake()
    {
        parentShip = GetComponentInParent<Ship>();
    }

    void Start()
    {
        parentShip.DeathAction += OnEnemyDie;
    }

    void OnEnemyDie()
    {
        if (parentShip.transform.position.IsWithinCameraBounds())
        {
            var collectible = CollectibleObjectPool.Instance.Get((int)CollectibleType.Score);

            collectible.transform.position = transform.position;
            collectible.gameObject.SetActive(true);
            collectible.enabled = true;
        }
    }

    void OnDestroy()
    {
        parentShip.DeathAction -= OnEnemyDie;
    }
}