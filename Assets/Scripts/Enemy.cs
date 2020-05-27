using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10f;
    public float StartHealth = 100f;
    private float _health;
    public int goldValue = 50;
    private Transform target;
    private int wavepointIndex = 0;

    [Header("Опционально")] public Image heathBar;
    
    private void Start()
    {
        target = Waypoints.points[0];
        _health = StartHealth;
    }

    public void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        heathBar.fillAmount = _health / StartHealth;
        if (_health <= 0)
        {
            PlayerStats.Money += goldValue;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
      
        Destroy(gameObject);
    }
}