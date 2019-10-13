using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RestartGame();
        }
    }

    public Maze mazePrefab;

    private Maze mazeInstance;

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
        //mazeInstance.Generate();
    }

    private void RestartGame()
    {
        StopAllCoroutines();//Stop all currently running coroutines
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}
