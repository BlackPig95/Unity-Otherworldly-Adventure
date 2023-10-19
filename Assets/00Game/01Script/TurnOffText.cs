using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffText : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Observer.Instance.Notify(Observer.GuideTextOff);
    }
}
