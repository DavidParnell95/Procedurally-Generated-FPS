using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    //enemy object
    public GameObject theEnemy;

    //Max number of enemies
    public int maxEnemies;
    public int xpos, zpos;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyGen());
    }

    IEnumerator EnemyGen()
    {
        //While less than max 
        while(enemyCount < maxEnemies)
        {
            xpos = Random.Range(1, 240);
            zpos = Random.Range(1, 240);

            //Create enemy
            Instantiate(theEnemy, new Vector3(xpos,1.5f,zpos), Quaternion.identity);

            yield return new WaitForSeconds(0.1f);

            enemyCount += 1;
        }
    }

    
}
