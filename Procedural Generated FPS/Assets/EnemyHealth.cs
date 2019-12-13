//Simple script to handle enemy health
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;//Enemy Health 

    public void TakeDamage(float amount)
    {
        health -= amount;//Subtract damage from health

        //If out of health
        if (health <= 0f)
        {
            Die();//Die
        }
    }

    void Die()
    {
        Destroy(gameObject);//Destroy the enemy game object
    }
}