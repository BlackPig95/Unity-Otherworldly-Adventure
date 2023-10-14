using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
    public int saveHp;
    public Vector2 savePos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetSaveInfo(collision.gameObject);
    }
    public void GetSaveInfo(GameObject player)
    {
        saveHp = player.GetComponent<PlayerController>().playerHP;
        savePos = player.transform.position;
        SaveToJSON();
    }
    public void SaveToJSON()
    {
        SaveData data = new SaveData();
        data.Hp = saveHp;
        data.Pos = savePos;
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("GameSaved", json);
    }
    public void LoadFromJSON(GameObject player)
    {
        string json = PlayerPrefs.GetString("GameSaved");
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        player.transform.position = data.Pos;
        GameManager.Instance.playerController.playerHP = data.Hp;
    }
}
