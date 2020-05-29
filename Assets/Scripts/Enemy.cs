using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float StartSpeed = 10f;
    public int EnemyDamage = 1;
  
    [HideInInspector] public float speed;
    
    [Header("Unity Stuff")] public Image HealthBar;
    public float StartHealth = 100;
    private float health;

    public int Worth = 50;
    public GameObject DeathEffect;  // Я поставил сюда эффект от попадания, но для каждого врага можно свой эффект назначить
    private bool isDead = false;

    void Start()
    {
        speed = StartSpeed;
        health = StartHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        HealthBar.fillAmount = health / StartHealth;
        
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    /*Если нужно будет замедлять врага 
    public void Slow(float pct)  
    {
        speed = startSpeed * (1f - pct);
    }*/

    void Die()
    {
        isDead = true;
        PlayerStats.Money += Worth;
        PlayerStats.KillStat++;

        GameObject effect = (GameObject) Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}