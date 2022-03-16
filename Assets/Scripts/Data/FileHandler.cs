using System;
using System.IO;
using UnityEngine;

public static class FileHandler
{
    static readonly string settingsDirectoryPath = Path.Combine(Application.persistentDataPath, "Data");
    static readonly string settingsFilePath = Path.Combine(settingsDirectoryPath, "usersettings.json");

    public static void Load(this UserSettings userSettings)
    {
        Directory.CreateDirectory(settingsDirectoryPath);

        if (File.Exists(settingsFilePath))
        {
            try
            {
                using FileStream fs = new FileStream(settingsFilePath, FileMode.Open);
                using StreamReader sr = new StreamReader(fs);
                string data = sr.ReadToEnd();

                JsonUtility.FromJsonOverwrite(data, userSettings);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occurred when trying to load data from file \n{e}");
            }
        }
    }

    public static void Save(this UserSettings userSettings)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(settingsFilePath));

            var json = JsonUtility.ToJson(userSettings, true);

            using FileStream fs = new FileStream(settingsFilePath, FileMode.Create);
            using StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error occurred when trying to save data to file \n{e}");
        }
    }
}