using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] UserSettings userSettings;
    [SerializeField] GameObject[] playerPrefabs;

    void OnEnable()
    {
        Instantiate(playerPrefabs[userSettings.SelectedPlayer], transform.position, transform.rotation);
    }
}