
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //enemy health
    public float health = 50f;

    //If dealt damage
    public void TakeDamage(float amount)
    {
        health -= amount;//subtract damage from health

        //If health is gone, destroy enemy
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
