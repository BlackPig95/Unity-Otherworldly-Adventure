using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
     int saveHp;
     Vector2 savePos;
  //  SaveData startData;
    public void Init()
    {
    /*    startData = new SaveData();
        startData.Hp = GameManager.Instance.playerController.playerHP;
        startData.Pos = GameManager.Instance.playerController.transform.position;*/
        if (PlayerPrefs.HasKey(CONSTANT.prefSave))
            PlayerPrefs.DeleteKey(CONSTANT.prefSave); //No key should exist before player touch save point
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
            /*  player.transform.position = startData.Pos;
              GameManager.Instance.playerController.playerHP = startData.Hp;*/
            Observer.Instance.Notify(Observer.ReloadLevel);
            GameManager.Instance.InitLevel(0);
            return;
        }

        string json = PlayerPrefs.GetString(CONSTANT.prefSave);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            player.transform.position = data.Pos;
            GameManager.Instance.playerController.playerHP = data.Hp;
    }
}
