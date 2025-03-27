using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] GameObject[] bossPrefabs;

    void OnEnable()
    {
        Instantiate(bossPrefabs[selectedBossIndex.value], transform.position, transform.rotation);
    }
}