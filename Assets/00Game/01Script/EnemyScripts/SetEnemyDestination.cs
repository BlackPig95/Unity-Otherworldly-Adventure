using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SetEnemyDestination : MonoBehaviour
{
    [SerializeField] GameObject startPoint, endPoint;
    [SerializeField] Vector2 finishPoint;
    bool applicationQuitting = false;
    private void Start()
    {
        Observer.Instance.AddListener(Observer.FinishLevel, Destroy);
        startPoint.transform.parent = null;
        endPoint.transform.parent = null;
        finishPoint = startPoint.transform.position;
    }
    public Vector2 SetDestin()
    {
        if (Vector2.Distance(finishPoint, this.transform.position) <= 0.1f)
        {
            if (finishPoint == (Vector2)startPoint.transform.position)
                finishPoint = endPoint.transform.position;
            else finishPoint = startPoint.transform.position;
        }
        return finishPoint;
    }
    public void Destroy(object data = null)
    {
        startPoint.transform.parent = this.transform;
        endPoint.transform.parent = this.transform;
    }
    void OnDestroy()
    {
        if (!applicationQuitting)
            Observer.Instance.RemoveListener(Observer.FinishLevel, Destroy);
    }
    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }
}
