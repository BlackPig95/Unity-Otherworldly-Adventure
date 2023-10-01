using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void UpdatePlayerAnim(PlayerState _playerState)
    {
        for(int i = 0; i <= (int) PlayerState.Jump; i++)
        {
            if(_playerState == (PlayerState)i)
                animator.SetBool(_playerState.ToString(), true);
            else
                animator.SetBool(((PlayerState)i).ToString(), false);
        }
    }
    public void UpdatePlayerAnim(float playerVelocity)
    {
        animator.SetFloat("yVelocity", playerVelocity);
    }

    public void UpdateEnemyAnim(EnemyState _enemyState)
    {
        for(int i = 0; i <=(int) EnemyState.Hit; i++)
        {
            if(_enemyState == (EnemyState)i)
                animator.SetBool(_enemyState.ToString(), true);
            else animator.SetBool(((EnemyState)i).ToString(), false);
        }
    }
}
