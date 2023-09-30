using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = this.GetComponent<Animator>();
    }

    public void UpdatePlayerAnim(PlayerState _playerState)
    {
        for(int i = 0; i <= (int) PlayerState.Jump; i++)
        {
            if(_playerState == (PlayerState)i)
                Animator.SetBool(_playerState.ToString(), true);
            else
                Animator.SetBool(((PlayerState)i).ToString(), false);
        }
    }
    public void UpdatePlayerAnim(float playerVelocity)
    {
        Animator.SetFloat("yVelocity", playerVelocity);
    }

    public void UpdateEnemyAnim(EnemyState _enemyState)
    {

    }
}
