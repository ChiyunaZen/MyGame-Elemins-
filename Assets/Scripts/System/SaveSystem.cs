using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{

    private static string saveFilePath => Path.Combine(Application.persistentDataPath, "savefile.json");　//セーブデータを保存するファイル
    private static string initialDataFilePath => Path.Combine(Application.persistentDataPath, "initialData.json");//初期データ保存用ファイル



    // 初期データを設定するメソッド
    public static void SetInitialData(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(initialDataFilePath, jsonData);
        Debug.Log("初期データをセーブしました: " + initialDataFilePath);
    }

    public static void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonData);
        Debug.Log("セーブしました " + saveFilePath);

        if (!File.Exists(Path.Combine(Application.persistentDataPath, "initialData.json")))
        {
            //初期データが存在しない場合、現在の値を初期データとして保存する
            SetInitialData(data);
        }
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

    // 初期データをロードする
    public static GameData LoadInitialData()
    {
        if (File.Exists(initialDataFilePath))
        {
            string jsonData = File.ReadAllText(initialDataFilePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("初期データをロードしました");
            return data;
        }
        else
        {
            Debug.LogWarning("初期データが見つかりません");
            return null;
        }
    }

    // セーブデータを初期データで上書きする
    public static void ResetToInitialData()
    {
        GameData initialData = LoadInitialData();
        if (initialData != null)
        {
            SaveGame(initialData); // 初期データで上書き保存
            Debug.Log("セーブデータを初期化しました");
        }
        else
        {
            Debug.LogWarning("初期データが設定されていません");
        }
    }


    //public static void DeleteSaveData()
    //{
    //    if (File.Exists(saveFilePath))
    //    {
    //        File.Delete(saveFilePath);
    //        Debug.Log("セーブデータを削除しました");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("削除するセーブデータがありません");
    //    }
    //}
}
