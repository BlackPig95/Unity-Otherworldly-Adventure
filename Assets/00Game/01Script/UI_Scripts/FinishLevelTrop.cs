using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelTrop : MonoBehaviour
{
    [SerializeField] GameObject finishedLevelText;
    private bool applicationQuitting = false;
    private void Start()
    {
        if (finishedLevelText == null)
        {
            finishedLevelText = GameObject.Find(CONSTANT.finishLevelText).GetComponentInChildren<Text>(true).gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Observer.Instance.Notify(Observer.FinishLevel, finishedLevelText);
        GameManager.Instance.gameState = GameState.Pause;
        GameManager.Instance.PauseGame();
        Observer.Instance.Notify(Observer.InitLevel);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if(!applicationQuitting) //Avoid bug object not cleaned up when quit game
        Observer.Instance.RemoveListener(Observer.FinishLevel, UIManager.Instance.ShowText);
    }
    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }

}
