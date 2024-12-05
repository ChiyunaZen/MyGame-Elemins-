using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static string saveFilePath => Application.persistentDataPath + "/saveData.json";

    public static void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonData);
        Debug.Log("セーブしました " + saveFilePath);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("ロードしました");
            return data;
        }
        else
        {
            Debug.LogWarning("セーブデータが見つかりません");
            return null;
        }
    }

    public static void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("セーブデータを削除しました");
        }
        else
        {
            Debug.LogWarning("削除するセーブデータがありません");
        }
    }
}
