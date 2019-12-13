//Simple script to handle enemy health
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;//enemy speed
    public float stopDistance;//how close enemy can come
    public float retreatDistance;// where the player must be for enemy to retreat

    public Transform player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        //if too far away move towards player
        if(Vector2.Distance(transform.position,player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        //if too near stop
        else if(Vector2.Distance(transform.position, player.position) < stopDistance 
            && Vector2.Distance(transform.position, player.position)>retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if(Vector2.Distance(transform.position,player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        
    }
}
