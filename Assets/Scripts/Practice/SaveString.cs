using System;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class SaveString : MonoBehaviour
{
    public TMP_InputField name;
    public TMP_InputField surname;
    public TMP_InputField age;

    private SaveData saveData = new();

    public string saveFilePath;


    void Start()
    {

        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
     if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            saveData = JsonUtility.FromJson<SaveData>(loadPlayerData);

            name.text = saveData.jName;
            surname.text = saveData.jSurname;
            age.text = saveData.jAge;
        }
        else
        {
            Debug.Log("No se encontró el file.");
        }
    }

    public void SaveToFile()
    {
        saveData.jName = name.text;
        saveData.jSurname = surname.text;
        saveData.jAge = age.text;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(saveFilePath, json);

        Debug.Log("Se guardaron los datos.");
    }
}
