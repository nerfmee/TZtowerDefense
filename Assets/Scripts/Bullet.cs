using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public GameObject impactEffect;
    public float speed = 70f;

    public int damage = 50;
    
    public void Seek(Transform _target)// преследуем врага
    {
        target = _target;
    } 

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    private void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Damage(target);
        Destroy(gameObject);
    }

    private void Damage(Transform enemy)
    {
        
      Enemy _enemy= enemy.GetComponent<Enemy>();
      if (_enemy != null)
      {
          _enemy.TakeDamage(damage);
      }

    }
}
