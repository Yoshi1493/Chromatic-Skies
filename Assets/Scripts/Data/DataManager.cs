using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    [SerializeField] UserSettings userSettings;
    [SerializeField] TextMeshProUGUI persistentDataPath;

    void Start()
    {
        LoadData();
        persistentDataPath.text = System.IO.Path.Combine(Application.persistentDataPath, "Data", "usersettings.json");
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