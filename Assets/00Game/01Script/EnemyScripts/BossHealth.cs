using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour, ICanGetHit
{
    [SerializeField] Stats stat;
    [SerializeField] GameObject restartButton;
    int health;
    int currentHealth;
    [SerializeField] Slider healthBar;
    public bool isGettingHit = false, isInvicible = false;
    Animator bossAnimator;
    [SerializeField] GameObject spikeBall;

    // Start is called before the first frame update

    void Start()
    {
        health = stat.hp;
        currentHealth = health;
        bossAnimator = GetComponent<Animator>();
        bossAnimator.enabled = false;
    }
    private void Update()
    {
        Collider2D[] detectPlayer = Physics2D.OverlapCircleAll(this.transform.position, 5f);
        foreach(Collider2D colli in detectPlayer)
        {
            if (colli.CompareTag(CONSTANT.playerPhysicsLayer))
                bossAnimator.enabled = true;
        }
        healthBar.value = (float)health / stat.hp;
        BossAnim();
    }
    public void GetHit(int damage)
    {
        if (isInvicible)
            return;
        this.gameObject.layer = 7;//Boss layer
        isGettingHit = true;
        isInvicible = true;
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
        {
            GameManager.Instance.gameState = GameState.Pause;
            GameManager.Instance.PauseGame();
            restartButton.SetActive(true);
        }
    }
    public void RestartGame()
    {
        GameManager.Instance.gameState = GameState.Play;
        GameManager.Instance.PauseGame();
        SceneManager.LoadScene(0);
    }
    public void BossAnim()
    {
        if (isGettingHit)
            bossAnimator.SetBool(CONSTANT.bossGetHit, true);
        else bossAnimator.SetBool(CONSTANT.bossGetHit, false);
    }
    public void BossRetaliate()
    {
        for (int i = -1; i <= 1; i++)
        {
            GameObject bullet = ObjectPooling.Instance.GetObject(spikeBall);
            bullet.transform.position = this.transform.position;
            bullet.gameObject.SetActive(true);
            StartCoroutine(BulletLifeTime(bullet));
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(i * 5f, 10f);
            bullet.transform.parent = this.transform;
        }
    }
    IEnumerator BulletLifeTime(GameObject bullet)
    {
        yield return new WaitForSeconds(4f);
        bullet.gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 5f);
    }
}
