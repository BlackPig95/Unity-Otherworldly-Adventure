using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelTrop : MonoBehaviour
{
    [SerializeField] GameObject finishedLevelText;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Observer.Instance.Notify(Observer.FinishLevel, finishedLevelText);
        Time.timeScale = 0f;
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveListener(Observer.FinishLevel, UIManager.Instance.ShowText);
    }

}
