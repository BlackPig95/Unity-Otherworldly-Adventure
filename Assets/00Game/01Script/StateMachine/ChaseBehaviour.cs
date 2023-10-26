using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    public Transform playerPos;
    float jumpForceX = 3f;
    float jumpForceY = 10f;
    bool canJump = true;
    public Rigidbody2D rigi;
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
        Vector2 target = (Vector2)playerPos.position - rigi.position;

        if (Vector2.Distance(rigi.position, playerPos.position) >= 3f)
            rigi.AddForce(new Vector2(target.normalized.x * jumpForceX, jumpForceY), ForceMode2D.Impulse);
        else if (Vector2.Distance(rigi.position, playerPos.position) < 3f)
            rigi.AddForce(new Vector2(target.x, jumpForceY), ForceMode2D.Impulse);

        canJump = false;
        animator.SetBool("BossChase", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canJump = true;
    }
}
