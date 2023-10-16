using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
     int saveHp;
     Vector2 savePos;
    SaveData startData;
    string startJSON;
    private void Start()
    {
        startData = new SaveData();
        startData.Hp = GameManager.Instance.playerController.playerHP;
        startData.Pos = GameManager.Instance.playerController.transform.position;
        startJSON = JsonUtility.ToJson(startData);
    }
    public void GetSaveInfo(object player)
    {
        saveHp = GameManager.Instance.playerController.playerHP;
        savePos = GameManager.Instance.playerController.transform.position;
       /* GameObject pl = (GameObject)player;  //Second method
        savePos = pl.transform.position;*/
        SaveToJSON();
    }
    public void SaveToJSON()
    {
        SaveData data = new SaveData();
        data.Hp = saveHp;
        data.Pos = savePos;
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(CONSTANT.prefSave, json);
    }
    public void LoadFromJSON(GameObject player)
    {
        if (!PlayerPrefs.HasKey(CONSTANT.prefSave))
        {
            SaveData start = JsonUtility.FromJson<SaveData>(startJSON);
            player.transform.position = startData.Pos;
            GameManager.Instance.playerController.playerHP = startData.Hp;
            return;
        }

        string json = PlayerPrefs.GetString(CONSTANT.prefSave);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            player.transform.position = data.Pos;
            GameManager.Instance.playerController.playerHP = data.Hp;
    }
}
