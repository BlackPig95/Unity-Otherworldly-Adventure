using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    void Awake()
    {
        Observer.Instance.AddListener(Observer.FinishLevel, ShowText);
    }
    public void ShowText(object data)
    {
       GameObject textObject = (GameObject)data;
            textObject.SetActive(true);
    }
}
