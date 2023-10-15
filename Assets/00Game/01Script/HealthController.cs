using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, ICanGetHit
{
    [SerializeField] Stats stat;
    public int health;
    int currentHealth;
    EnemyAnimationController enemyAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        health = stat.hp;
        enemyAnimationController = this.GetComponentInChildren<EnemyAnimationController>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit(int damage)
    {
        this.health -= damage;
        Debug.Log("Enemy HP " + health);
        if (this.health < currentHealth)
        {
            currentHealth = this.health;
            enemyAnimationController.isGettingHit = true;
        }
        CheckDead();
    }
    void CheckDead()
    {
        if (this.health <= 0)
            Destroy(this.gameObject);
    }
}
