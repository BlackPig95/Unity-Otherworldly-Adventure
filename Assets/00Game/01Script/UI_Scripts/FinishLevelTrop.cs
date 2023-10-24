using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelTrop : MonoBehaviour
{
    private bool applicationQuitting = false;
    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Observer.Instance.Notify(Observer.FinishLevel);
        Debug.Log("Finished");
        GameManager.Instance.gameState = GameState.Pause;
        GameManager.Instance.PauseGame();
        StartCoroutine(WaitFinish());
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
    IEnumerator WaitFinish()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Observer.Instance.Notify(Observer.InitLevel);
        Destroy(this.gameObject);
    }
}
