using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] UserSettings userSettings;

    void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        userSettings.Load();
    }

    public void SaveData()
    {
        userSettings.Save();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
}