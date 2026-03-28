using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI persistentDataPath;

    public UserData UserData {get; private set;}
    List<ISavable> savableObjects;

    public static DataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one (1) DataManager in the scene.");
        }

        Instance = this;
    }

    void Start()
    {
        savableObjects = new List<ISavable>(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISavable>());
        LoadData();

        //debug; remove later
        persistentDataPath.text = $"Settings data filepath:{'\n'}{System.IO.Path.Combine(Application.persistentDataPath, "Data", "usersettings.json")}";
    }

    void NewData()
    {
        UserData = new UserData();
    }

    public void LoadData()
    {
        UserData = FileHandler.Load(false);

        if (UserData == null)
        {
            NewData();
        }

        foreach (var item in savableObjects)
        {
            item.LoadData(UserData);
        }
    }

    public void SaveData()
    {
        foreach (var item in savableObjects)
        {
            item.SaveData(UserData);
        }

        UserData.Save(false);
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
}

public interface ISavable
{
    public void LoadData(UserData data);
    public void SaveData(UserData data);
}