using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI persistentDataPath;

    UserData userData;
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
        savableObjects = new List<ISavable>(FindObjectsOfType<MonoBehaviour>().OfType<ISavable>());
        LoadData();

        //debug; remove later
        persistentDataPath.text = $"Settings data filepath:{'\n'}{System.IO.Path.Combine(Application.persistentDataPath, "Data", "usersettings.json")}";
    }

    void NewData()
    {
        userData = new UserData();
    }

    public void LoadData()
    {
        userData = FileHandler.Load(false);

        if (userData == null)
        {
            NewData();
        }

        foreach (var item in savableObjects)
        {
            item.LoadData(userData);
        }
    }

    public void SaveData()
    {
        foreach (var item in savableObjects)
        {
            item.SaveData(userData);
        }

        userData.Save(false);
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