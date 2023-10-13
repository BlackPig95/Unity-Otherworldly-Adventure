using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SetEnemyDestination : MonoBehaviour
{
    [SerializeField] GameObject startPoint, endPoint;
    [SerializeField] Vector2 finishPoint;
    private void Start()
    {
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
}
