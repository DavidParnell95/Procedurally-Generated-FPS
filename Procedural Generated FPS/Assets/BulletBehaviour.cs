using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float damage = 30f;
    public GameObject impactEffect;

    private void OnTriggerEnter(Collider other)
    {
        print("hit: " + other.name);
        Destroy(gameObject);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
          
            enemy.TakeDamage(damage);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, .5f);
        }

    }
}
