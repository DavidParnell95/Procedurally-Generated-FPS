﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;
    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    // Coroutine version, slows down generation so it can be observed
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[sizeX, sizeZ];

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                yield return delay;
                CreateCell(x, z);
            }
        }
    }

    /***** Instant Version, non coroutine ******
    public void Generate()
    {
        
        cells = new MazeCell[sizeX,sizeZ];

        for(int x =0; x<sizeX;x++)
        {
            for(int z = 0; z<sizeZ;z++)
            {
                CreateCell(x, z);
            }
        }
     }
     ********************************************/



    

private void CreateCell(int x, int z)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[x, z] = newCell;

        newCell.name = "Maze Cell" + x + "," + z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - sizeX * .5f + .5f,
            0f,
            z - sizeZ * .5f + .5f);
    }
}
