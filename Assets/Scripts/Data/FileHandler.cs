using System;
using System.IO;
using UnityEngine;

public static class FileHandler
{
    static readonly string settingsDirectoryPath = Path.Combine(Application.persistentDataPath, "Data");
    static readonly string settingsFilePath = Path.Combine(settingsDirectoryPath, "usersettings.json");

    static readonly string encryptionString = "jCqJU7DBqNbDtxFtWsrmaWyyjyjO9xb";

    public static UserData Load(bool useEncryption)
    {
        UserData loadedData = null;

        if (File.Exists(settingsFilePath))
        {
            try
            {
                string data = "";

                using FileStream fs = new(settingsFilePath, FileMode.Open);
                {
                    using StreamReader sr = new(fs);
                    {
                        data = sr.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    data = EncryptDecrypt(data);
                }

                loadedData = JsonUtility.FromJson<UserData>(data);
                JsonUtility.FromJsonOverwrite(data, loadedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occurred when trying to load data from file \n{e}");
            }
        }

        return loadedData;
    }

    public static void Save(this UserData userData, bool useEncryption)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(settingsFilePath));

            var json = JsonUtility.ToJson(userData, true);

            if (useEncryption)
            {
                json = EncryptDecrypt(json);
            }

            using FileStream fs = new(settingsFilePath, FileMode.Create);
            {
                using StreamWriter sw = new(fs);
                {
                    sw.Write(json);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error occurred when trying to save data to file \n{e}");
        }
    }

    static string EncryptDecrypt(string s)
    {
        string modifiedData = "";

        for (int i = 0; i < s.Length; i++)
        {
            modifiedData += (char)(s[i] ^ encryptionString[i % encryptionString.Length]);
        }

        return modifiedData;
    }
}