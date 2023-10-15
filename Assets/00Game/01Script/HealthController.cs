using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, ICanGetHit
{
    [SerializeField] Stats stat;
    int health;
    int currentHealth;
    EnemyAnimationController enemyAnimationController;
    [SerializeField] Slider healthBar;
    // Start is called before the first frame update

    void Start()
    {
        health = stat.hp;
        enemyAnimationController = this.GetComponentInChildren<EnemyAnimationController>();
        currentHealth = health;
    }
    private void Update()
    {
        healthBar.value = (float) health / stat.hp; // Null ref exception??
    }
    private void LateUpdate()
    {
        healthBar.transform.localScale = -this.gameObject.transform.localScale;
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
