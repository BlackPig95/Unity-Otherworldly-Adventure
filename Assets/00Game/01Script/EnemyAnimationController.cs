using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    AnimationController animationController;
    EnemyState enemyState = EnemyState.Walk;
    bool isRunning = false;
    public bool isGettingHit = false;
    DetectPlayerWithRay detectPlayerWithRay;
    // Start is called before the first frame update
    void Start()
    {
        animationController = this.GetComponentInChildren<AnimationController>();
        animationController.eventAnim += HitAnimEvent;
        detectPlayerWithRay = this.GetComponentInParent<DetectPlayerWithRay>();
    }

    // Update is called once per frame
    void Update()
    {
        animationController.UpdateEnemyAnim(enemyState);
        SetEnemyState();
        if(detectPlayerWithRay != null) 
        isRunning = detectPlayerWithRay.DetectPlayer();
    }
    public void HitAnimEvent(string name) //Attached to event action
    {
        if (name == CONSTANT.hitEvent)
        {
            isGettingHit = false;
            enemyState = EnemyState.Walk;
            Debug.Log(name);
        }
    }
    void SetEnemyState()
    {
        if (isRunning)
            enemyState = EnemyState.Run;
        else enemyState = EnemyState.Walk;
        if (isGettingHit)
            enemyState = EnemyState.Hit;
    }
}
