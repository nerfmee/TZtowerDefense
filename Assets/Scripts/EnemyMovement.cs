using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform _target;
    private int _wavepointIndex = 0;

    private Enemy _enemy;

    void Start()
    {
        _enemy = GetComponent<Enemy>();

        _target = Waypoints.Points[0];
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        _enemy.speed = _enemy.StartSpeed;
    }

    void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.Points.Length - 1)
        {
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.Points[_wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives-= _enemy.EnemyDamage;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

}