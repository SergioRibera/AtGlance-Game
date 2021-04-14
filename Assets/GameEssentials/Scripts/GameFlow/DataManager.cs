using System;
using UnityEngine;

using EasyDataSave;

public static class DataManager {

    static string DATA_PATH = Application.persistentDataPath;
    const string KEY_ENCRYPT = "KL-AM/BCUL&LYUP_W_YULDCJBCV/ÑVM&ÑXOGNKDNNIWTHLS";

    static DataPlayer data;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init() =>
        data = SaveDataManager.Exist(typeof(DataPlayer), DATA_PATH) ?
            SaveDataManager.Load<DataPlayer>(DATA_PATH, KEY_ENCRYPT, loadPrivates: true) :
            new DataPlayer();

    static void Save() => data.Save<DataPlayer>(DATA_PATH, KEY_ENCRYPT, savePrivates: true);

    // Getters
    public static string GetName => data.name;
    public static int GetMaxScore => data.maxScore;
    public static int GetIdSkin => data.idSkin;

    // Setters
    public static string SetName { set { data.name = value; Save(); } }
    public static int SetMaxScore { set { data.maxScore = value; Save(); } }
    public static int SetIdSkin { set { data.idSkin = value; Save(); } }
}
