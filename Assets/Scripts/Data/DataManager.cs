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
        userSettings.Load(false);
    }

    public void SaveData()
    {
        userSettings.Save(false);
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
}