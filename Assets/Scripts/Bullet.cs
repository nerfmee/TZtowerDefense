using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public GameObject ImpactEffect;
    public float Speed = 10;

    public int BulletDamage = 50;
    
    public void Seek(Transform _target)
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
        float distanceThisFrame = Speed * Time.deltaTime;

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
        GameObject effectIns = Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Damage(target);
        Destroy(gameObject);
    }

    private void Damage(Transform enemy)
    {
        
      Enemy _enemy= enemy.GetComponent<Enemy>();
      if (_enemy != null)
      {
          _enemy.TakeDamage(BulletDamage);
      }

    }
}
