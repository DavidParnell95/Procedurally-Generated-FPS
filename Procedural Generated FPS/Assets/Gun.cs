
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;

    public Camera cam;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire 1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        //Check if raycast from current look direction hits 
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            muzzleFlash.Play();

            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Enemy>();

            //If a valid target found, deal damage
            if(target != null)
            {
                target.TakeDamage(Damage);
            }
        }
    }
}
