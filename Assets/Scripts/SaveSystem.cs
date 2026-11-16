using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour{
    public static SaveSystem instance { get; private set; }

    private void Awake(){
        instance = this;
        
    }
    public void CreateSave(){
        SaveDataModel model = new SaveDataModel();

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void SaveBestTime(float time){
        SaveDataModel model = new SaveDataModel();

        if (time > model.BestTime) model.BestTime = time;

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public float GetBestTime(){
        SaveDataModel model = JsonUtility.FromJson<SaveDataModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        return model.BestTime;
    }
}

public class SaveDataModel{
    public float BestTime = 0;
}