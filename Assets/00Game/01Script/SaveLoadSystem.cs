using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
    public int saveHp;
    public Vector2 savePos;

    public void GetSaveInfo(object player)
    {
        saveHp = GameManager.Instance.playerController.playerHP;
        savePos = GameManager.Instance.playerController.transform.position;
       /* GameObject pl = (GameObject)player;  Second method
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
            return;

            string json = PlayerPrefs.GetString(CONSTANT.prefSave);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            player.transform.position = data.Pos;
            GameManager.Instance.playerController.playerHP = data.Hp;
        
       
    }
}
