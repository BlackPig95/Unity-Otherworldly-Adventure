using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
    public int saveHp;
    public float savePosX;
    public float savePosY;
    public float savePosZ;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetSaveInfo(collision.gameObject);
    }
    public void GetSaveInfo(GameObject player)
    {
        savePosX = player.transform.position.x;
        savePosY = player.transform.position.y;
        savePosZ = player.transform.position.z;
        SaveToJSON();
    }
    public void SaveToJSON()
    {
        SaveData data = new SaveData();
        data.Hp = saveHp;
        data.PosX = savePosX;
        data.PosY = savePosY;
        data.PosZ = savePosZ;
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("GameSaved", json);
    }
    public void LoadFromJSON(GameObject player)
    {
        string json = PlayerPrefs.GetString("GameSaved");
        SaveData data = JsonUtility.FromJson<SaveData>(json);
         player.transform.position = new Vector3(data.PosX, data.PosY, data.PosZ);
    }
}
