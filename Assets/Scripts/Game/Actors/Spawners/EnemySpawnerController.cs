using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] GameObject[] bossPrefabs;

    void Awake()
    {
        Transform transform = GetComponent<Transform>();
        Instantiate(bossPrefabs[selectedBossIndex.value], transform.position, transform.rotation);
    }
}