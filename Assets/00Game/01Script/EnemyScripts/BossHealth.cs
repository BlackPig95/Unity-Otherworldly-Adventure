using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour, ICanGetHit
{
    [SerializeField] Stats stat;
    int health;
    int currentHealth;
    [SerializeField] Slider healthBar;
    public bool isGettingHit = false;
    Animator bossAnimator;

    // Start is called before the first frame update

    void Start()
    {
        health = stat.hp;
        currentHealth = health;
        bossAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        healthBar.value = (float)health / stat.hp;
        BossAnim();
    }
    public void GetHit(int damage)
    {
        isGettingHit = true;
        this.health -= damage;
        Debug.Log("Enemy HP " + health);
        if (this.health < currentHealth)
        {
            currentHealth = this.health;
        }
        CheckDead();
    }
    void CheckDead()
    {
        if (this.health <= 0)
            Destroy(this.gameObject);
    }
    public void BossAnim()
    {
        if (isGettingHit)
            bossAnimator.SetBool(CONSTANT.bossGetHit,true);
    }

}
