using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] IntObject selectedPlayerIndex;
    [SerializeField] GameObject[] playerPrefabs;

    void OnEnable()
    {
        Instantiate(playerPrefabs[selectedPlayerIndex.value], transform.position, transform.rotation);
    }
}