using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSavePoint : MonoBehaviour
{
    private void Start()
    {
        Observer.Instance.AddListener(Observer.SavePoint, SaveLoadSystem.Instance.GetSaveInfo);
        if(PlayerPrefs.HasKey(CONSTANT.prefSave))
        PlayerPrefs.DeleteKey(CONSTANT.prefSave);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Observer.Instance.Notify(Observer.SavePoint, collision);
    }
}
