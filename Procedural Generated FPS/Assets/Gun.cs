
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;//damage dealt per shot
    public float range = 100f;//Damage range 

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Fire1"))
        {
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
        }
    }
}
