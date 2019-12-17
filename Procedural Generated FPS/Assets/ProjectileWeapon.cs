using System.Collections;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    
    public GameObject bulletPref;
    public Transform bulletSpawn;

    public float speed = 50;
    public float lifeTime = 2;

    public float fireRate = 15f;//Fire rate

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nexTimeToFire = 0f;//When you can shoot next 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        muzzleFlash.Play();

        GameObject bullet = Instantiate(bulletPref);

        //Ignore collision with weapon
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
            bulletSpawn.parent.GetComponent<Collider>()
            );

        bullet.transform.position = bulletSpawn.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        //only apply one force to the bullet 
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * speed, ForceMode.Impulse);

        //Destroy bullet after time 
        StartCoroutine(DestroyBullet(bullet, lifeTime));
    }

    private IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }

}

