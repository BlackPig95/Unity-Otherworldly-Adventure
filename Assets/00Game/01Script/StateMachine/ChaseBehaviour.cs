using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    public Transform playerPos;
    bool canJump = true;
    public Rigidbody2D rigi;
    float maxHeight = 6f, maxRange = 4f, g = 9.81f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigi = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!canJump)
            return;
        Debug.Log("Jump");
        float distance = Mathf.Abs(playerPos.position.x - rigi.position.x);
        Vector2 direction = (Vector2)playerPos.position - rigi.position;
        /*    H = v ^ 2 * sin2a / g; R = v ^ 2 * sin2a / 2g => R * sin ^ 2a = H * sin2a => sin ^ 2a / sin2a = H / R
                   => sin ^ 2a / 2sinacosa = H / R => tana = 2H / R => a = atan(2H / R)
                   => v ^ 2 = Rg / sin2a; vx ^ 2 = v ^ 2 - vy ^ 2; vy = 10*/
        if (distance >= maxRange)
        {
            float angle = Mathf.Atan(2 * maxHeight / maxRange);
            float initialVelocity = Mathf.Sqrt(Mathf.Abs(maxRange * g / Mathf.Sin(2 * angle)));
            float yVelocity = initialVelocity * Mathf.Sin(angle);//Angle is in rad
            float xVelocity = initialVelocity * Mathf.Cos(angle);
            //If x distance is bigger than maxRange unit => Jump maxRange unit
            rigi.AddForce(new Vector2(direction.normalized.x * xVelocity, yVelocity), ForceMode2D.Impulse);
        }
        else if (distance < maxRange)
        {
            float angle = Mathf.Atan(2 * maxHeight / distance);
            float initialVelocity = Mathf.Sqrt(Mathf.Abs(distance * g / Mathf.Sin(2 * angle)));
            float yVelocity = initialVelocity * Mathf.Sin(angle);
            float xVelocity = initialVelocity * Mathf.Cos(angle);
            //If x distance is smaller than maxRange unit => Jump on player's head
            rigi.AddForce(new Vector2(direction.normalized.x * xVelocity, yVelocity), ForceMode2D.Impulse);
        }

        canJump = false;
        animator.SetBool("BossChase", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canJump = true;
    }
}
