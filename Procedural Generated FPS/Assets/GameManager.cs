using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Maze mazePrefab;
    public Player playerPrefab;

    private Maze mazeInstance;
    private Player playerInstance;


    // Start is called before the first frame update
    void Start()
    {
        //Start Game
        StartCoroutine(BeginGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RestartGame();
        }
    }

    private IEnumerator BeginGame()
    {
        //Clear Skybox
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);

        //Create Maze
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());

        //Create Player
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));//Spawn player in random location

        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }

    private void RestartGame()
    {
        StopAllCoroutines();//Stop all running coroutines
        Destroy(mazeInstance.gameObject);//Destroy current maze

        //If player exists
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);//Destroy player
        }
        StartCoroutine(BeginGame());//Restart game
    }
}
