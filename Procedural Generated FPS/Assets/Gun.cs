
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;//damage dealt per shot
    public float range = 100f;//Damage range 
    public float fireRate = 15f;//Fire rate

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nexTimeToFire = 0f;//When you can shoot next 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
        { 
            nexTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;

        //Checks if raycast, projected from camera's current position (in range) hits a target 
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, .5f);
        }
    }
}
